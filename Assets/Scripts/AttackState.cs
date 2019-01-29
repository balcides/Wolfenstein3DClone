using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyAI {

	EnemyStates enemy;
	float timer;

	//contructor for enemy
	public AttackState(EnemyStates enemy){
		this.enemy = enemy;
	}

	public void UpdateActions(){
		timer += Time.deltaTime;
		float distance = Vector3.Distance (enemy.chaseTarget.transform.position, enemy.transform.position);

		if (distance > enemy.attackRange && enemy.onlyMelee == true) {
			ToChaseState ();
		}

		if (distance > enemy.shootRange && enemy.onlyMelee == false) {
			ToChaseState ();
		}

		Watch ();

		if(distance <= enemy.shootRange && distance > enemy.attackRange && enemy.onlyMelee == false && timer >= enemy.attackDelay){
			Attack(true);
			timer = 0;
		}
		if (distance <= enemy.attackRange && timer >= enemy.attackDelay) {
			Attack (false);
			timer = 0;
		}
	}
		
	void Attack(bool shoot){
		if (!shoot) {
			enemy.chaseTarget.SendMessage ("EnemyHit", enemy.meleeDamage, SendMessageOptions.DontRequireReceiver);

		} else if (shoot){
			GameObject missle = GameObject.Instantiate (enemy.missle, enemy.transform.position, Quaternion.identity);
			missle.GetComponent<Missile> ().speed = enemy.missileSpeed;
			missle.GetComponent<Missile> ().damage = enemy.missileDamage;
		}
	}

	void Watch(){
		RaycastHit hit;
		if (Physics.Raycast (enemy.transform.position, enemy.vision.forward, out hit, enemy.patrolRange, enemy.raycastMask) &&
		    hit.collider.CompareTag ("Player")) {
			enemy.chaseTarget = hit.transform;
			enemy.lastKnownPosition = hit.transform.position;
		} else {
			ToAlertState ();
		}
	}


	public void OnTriggerEnter(Collision enemy){
	}

	public void ToPatrolState (){
		//we cannot
	}

	public void ToAttackState(){
		//we cannot
	}

	public void ToAlertState(){
		enemy.currentState = enemy.alertState;
	}

	public void ToChaseState(){
		enemy.currentState = enemy.chaseState;
	}
}
