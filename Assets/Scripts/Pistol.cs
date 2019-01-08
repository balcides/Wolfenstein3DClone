using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	void Awake(){

		source = GetComponent<AudioSource> ();
	}

	void FixedUpdate(){

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit; 

		//if fire button pressed
		if (Input.GetButtonDown ("Fire1")) {

			//play fire sound
			source.PlayOneShot (shotSound);
			StartCoroutine ("shot");

			//if ray hits an object
			if(Physics.Raycast(ray, out hit, pistolRange)){
				Debug.Log("I've collided with" + hit.collider.gameObject.name);
			}


		}
	}

	//shot function for pistol
	IEnumerator Shot(){

		//quick sprite anim sequential
		GetComponent<SpriteRenderer> ().sprite = shotPistol;
		yield return new WaitForSeconds(0.1f);
		GetComponent<SpriteRenderer>().sprite = idlePistol;
	}


	
}
