using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*

	Pistol.cs

	Retro FPS style game mechanics for psitol.
	

	Author: Gabe 
	Original Content: Fabryka Twórców Gier
	Based on tutorial: https://youtu.be/799W_Hz68rw

 */

[RequireComponent(typeof(AudioSource))]
public class Pistol : MonoBehaviour {

	public GameObject bloodSplat;
	public float shootOffset = 3.0f;
	public Sprite idlePistol;
	public Sprite shotPistol;
	public float pistolDamage;
	public float pistolRange; 		//limit how far we can shoot
	public AudioClip shotSound;
	AudioSource source;
	public AudioClip reloadSound;
	public AudioClip emptyGunSound;

	public int ammoAmount = 10;
	public int ammoClipSize;

	public Text ammoText;

	int ammoLefts;
	int ammoClipLeft;

	bool isShot = false;
	bool isReloading = false;

	public GameObject bulletHole;


	void Awake(){

		source = GetComponent<AudioSource> ();
		ammoLefts = ammoAmount;
		ammoClipLeft = ammoClipSize;
	}

	private void OnEnable(){
		isReloading = false;
	}

	void Update(){

		ammoText.text = ammoClipLeft + " / " + ammoLefts;

		//fire 
		if(Input.GetButtonDown("Fire1") && !isReloading){
			isShot = true;
		}

		//reload
		if (Input.GetKeyDown (KeyCode.R) && !isReloading && ammoClipLeft != ammoClipSize) {
			Reload ();
		}
	}


	void FixedUpdate(){

		//original script that shoots from mouse pos to object but it's not as accurate as using screen rez math
		//Vector3 shootPos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y + shootOffset, Input.mousePosition.z);

		//saved position for shooting dead center without spread
		Vector3 deadCenter = new Vector3(Screen.width/2, Screen.height/2,0);

		//chooses a random point within a 2D circle (super handy) and then usses or crosshair spread as a value for it's radius
		Vector2 bulletOffset = Random.insideUnitCircle * DynamicCrosshair.spread;

		//spread position
		Vector3 randomTarget = new Vector3(deadCenter.x + bulletOffset.x, deadCenter.y + bulletOffset.y, 0);
			
		Ray ray = Camera.main.ScreenPointToRay (randomTarget);
		RaycastHit hit; 
	
		//when shot is fired
		if (isShot && ammoClipLeft > 0 && !isReloading) {

			isShot = false;
			DynamicCrosshair.spread += DynamicCrosshair.PISTOL_SHOOTING_SPREAD;
			ammoClipLeft--;

			//play fire sound
			source.PlayOneShot (shotSound);
			StartCoroutine ("Shot");

			//if ray hits an object
			if (Physics.Raycast (ray, out hit, pistolRange)) {

				//if we hit the enemy, run collider patrol and damage script
				if (hit.transform.CompareTag ("Enemy")) {

					Instantiate (bloodSplat, hit.point, Quaternion.identity);

					if (hit.collider.gameObject.GetComponent<EnemyStates> ().currentState == hit.collider.gameObject.GetComponent<EnemyStates> ().patrolState ||
					    hit.collider.gameObject.GetComponent<EnemyStates> ().currentState == hit.collider.gameObject.GetComponent<EnemyStates> ().alertState) {

						Debug.Log ("hidden shot called");
						hit.collider.gameObject.SendMessage ("HiddenShot", transform.parent.transform.position, SendMessageOptions.DontRequireReceiver);
					}

					//send message to start a fucntion when ray hits surface, passes command for pistol damage on surfaces
					Debug.Log ("I've collided with: " + hit.collider.gameObject.name);

					hit.collider.gameObject.SendMessage ("AddDamage", pistolDamage, SendMessageOptions.DontRequireReceiver);

				} else {
					
					//create bullethole and parent to collided object's position so it sticks
					Instantiate (bulletHole, hit.point, Quaternion.FromToRotation (Vector3.up, hit.normal)).transform.parent = hit.collider.gameObject.transform;
				}


			}

		//if shot and no ammo, reload
		} else if (isShot && ammoClipLeft <= 0 && !isReloading) {
			isShot = false;
			Reload();
		}
	}


	void Reload(){
	/*

		Handles gun reload and ammo clip

 	*/
		
		int bulletsToReload = ammoClipSize - ammoClipLeft;

		if (ammoLefts >= bulletsToReload) {
			StartCoroutine ("ReloadWeapon");
			ammoLefts -= bulletsToReload;
			ammoClipLeft = ammoClipSize;

		} else if (ammoLefts < bulletsToReload && ammoLefts > 0) {
			StartCoroutine ("ReloadWeapon");
			ammoClipLeft += ammoLefts;
			ammoLefts = 0;

		} else if (ammoLefts <= 0) {
			source.PlayOneShot (emptyGunSound);
		}
	}


	IEnumerator ReloadWeapon(){
	/*

		Reloads weapon

 	*/
		isReloading = true;
		source.PlayOneShot (reloadSound);
		yield return new WaitForSeconds(2);
		isReloading = false;

	}


	IEnumerator Shot(){
	/*

		Shot function for pistol

 	*/

		//quick sprite anim sequential
		GetComponent<SpriteRenderer> ().sprite = shotPistol;
		yield return new WaitForSeconds(0.1f);
		GetComponent<SpriteRenderer>().sprite = idlePistol;
	}


	
}
