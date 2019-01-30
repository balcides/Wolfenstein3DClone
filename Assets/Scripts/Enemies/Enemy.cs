using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*

	Enemy.cs

	Script for Enemy behavior and manager (Base Class). Common functions and variables for all the enemies.

	Author: Gabe 
	Original Content: Fabryka Twórców Gier
	Based on tutorial: https://youtu.be/799W_Hz68rw

 */


public class Enemy : MonoBehaviour {

	public Sprite deadBody;
	public int maxHealth;
	public float health;

	EnemyStates es;
	NavMeshAgent nma;
	SpriteRenderer sr;
	BoxCollider bc;

	private void Start(){
		health = maxHealth;
		es = GetComponent<EnemyStates> ();
		nma = GetComponent<NavMeshAgent> ();
		sr = GetComponent<SpriteRenderer> ();
		bc = GetComponent<BoxCollider> ();
	}

	void PistolHit(float damage){
		health -= damage;
	}

	private void Update(){
		if (health <= 0) {
			es.enabled = false;
			nma.enabled = false;
			sr.sprite = deadBody;
			bc.center = new Vector3 (0, -0.8f, 0);
			bc.size = new Vector3 (1.05f, -0.43f, 0.2f);
		}
	}

	

}
