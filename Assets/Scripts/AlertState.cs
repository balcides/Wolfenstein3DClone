using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : EnemyAI {

	EnemyStates enemy;
	float timer;

	//contructor for enemy
	public AlertState(EnemyStates enemy){
		this.enemy = enemy;
	}

	public void UpdateActions(){
		Search ();
		Watch ();
		if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance) {
			LookAround ();
		}
	}

	void Watch(){
		RaycastHit hit;
		if (Physics.Raycast (enemy.transform.position, enemy.vision.forward, out hit, enemy.patrolRange) &&
			hit.collider.CompareTag("Player")) {
			enemy.chaseTarget = hit.transform;
			enemy.navMeshAgent.destination = hit.transform.position;
			ToChaseState ();
		}
	}

	void LookAround(){
		timer += Time.deltaTime;
		if (timer >= enemy.stayAlertTime) {
			timer = 0;
			ToPatrolState ();
		}
	}

	void Search(){
		enemy.navMeshAgent.destination = enemy.lastKnownPosition;
		//enemy.navMeshAgent.Resume ();
		enemy.navMeshAgent.isStopped = false;
	}

	public void OnTriggerEnter(Collision enemy){
	}

	public void ToPatrolState (){
		enemy.currentState = enemy.patrolState;
	}

	public void ToAttackState(){
		Debug.Log ("Error");
	}

	public void ToAlertState(){
		Debug.Log ("Error");
	}

	public void ToChaseState(){
		enemy.currentState = enemy.chaseState;
	}
}
