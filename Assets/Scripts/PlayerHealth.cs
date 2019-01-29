using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

	public int maxHealth;
	public AudioClip hit;
	public FlashScreen flash;
	AudioSource source;
	float health;


	// Use this for initialization
	void Start () {

		health = maxHealth;
		source = GetComponent<AudioSource> ();
		
	}
	
	void EnemyHit(float damage){

		Debug.Log ("Enemy hit happening");
		source.PlayOneShot (hit);
		health -= damage;
		flash.TookDamage ();
	}
}
