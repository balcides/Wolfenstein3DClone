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

	public Sprite idlePistol;
	public Sprite shotPistol;
	public float pistolDamage;
	public float pistolRange; 		//limit how far we can shoot
	public AudioClip shotSound;
	AudioSource source;
	public AudioClip reloadSound;
	public AudioClip emptyGunSound;

	public int ammoAmount;
	public int ammoClipSize;

	public Text ammoText;

	int ammoLefts;
	int ammoClipLeft;

	bool isShot = false;
	bool isReloading = false;


	void Awake(){

		source = GetComponent<AudioSource> ();
		ammoLefts = ammoAmount;
		ammoClipLeft = ammoClipSize;
	}


	void Update(){

		ammoText.text = ammoClipLeft + " / " + ammoLefts;

		//fire 
		if(Input.GetButtonDown("Fire1")){
			isShot = true;
		}

		//reload
		if (Input.GetKeyDown (KeyCode.R)) {
			Reload ();
		}
	}


	void FixedUpdate(){

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit; 
	
		//when shot is fired
		if (isShot && ammoClipLeft > 0 && !isReloading) {
			isShot = false;
			ammoClipLeft--;

			//play fire sound
			source.PlayOneShot (shotSound);
			StartCoroutine ("Shot");

			//if ray hits an object
			if (Physics.Raycast (ray, out hit, pistolRange)) {

				//send message to start a fucntion when ray hits surface, passes command for pistol damage on surfaces
				Debug.Log ("I've collided with: " + hit.collider.gameObject.name);
				hit.collider.gameObject.SendMessage ("pistorlHit", pistolDamage, SendMessageOptions.DontRequireReceiver);
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
