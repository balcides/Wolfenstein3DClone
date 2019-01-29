using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyAI{

	EnemyStates enemy;
	int nextWayPoint = 0;

	//contructor for enemy
	public PatrolState(EnemyStates enemy){
		this.enemy = enemy;
	}

	public void UpdateActions(){
		Watch ();
		Patrol ();
	}

	void Watch(){
		RaycastHit hit; 

		//if the raycast hit from enemy forward
		if (Physics.Raycast (enemy.transform.position, -enemy.transform.forward, out hit, enemy.patrolRange)) {
			if (hit.collider.CompareTag ("Player")) {
				
				//enemy.chaseTarget = hit.transform; 
				Debug.Log(enemy.name + " Enemy hits player target on patrol! " + hit.collider.name);
				ToChaseState ();
			}
		}
	}

	void Patrol(){

		//during patrol, enemy waypoint changes position based on next waypoint
		enemy.navMeshAgent.destination = enemy.waypoints [nextWayPoint].position;

		// keep moving the enemy
		//enemy.navMeshAgent.Resume ();
		enemy.navMeshAgent.isStopped = false;

		//if enemy distance less or equal to stopping ditance and pathPending is false
		if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && !enemy.navMeshAgent.pathPending) {

			//move to next waypoint by remainder of length (and then stop)
			nextWayPoint = (nextWayPoint + 1) % enemy.waypoints.Length;
		}

	}

	public void OnTriggerEnter(Collision enemy){

		//when player triggers collision, go to alert state
		if (enemy.gameObject.CompareTag ("Player")) {
			ToAlertState ();
		}
	}

	public void ToPatrolState (){
		
		Debug.Log("I am protrolling already!");

	}

	public void ToAttackState(){
		enemy.currentState = enemy.attackState;
	}

	public void ToAlertState(){
		enemy.currentState = enemy.alertState;
	}

	public void ToChaseState(){
		enemy.currentState = enemy.chaseState;
	}

}
