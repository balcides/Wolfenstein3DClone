﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : EnemyAI{

	EnemyStates enemy;

	//contructor for enemy
	public ChaseState(EnemyStates enemy){
		this.enemy = enemy;
	}

	public void UpdateActions(){
		Watch ();
		Chase ();
	}


	void Watch(){
		
		RaycastHit hit;
		if (Physics.Raycast (enemy.transform.position, enemy.vision.forward, out hit, enemy.patrolRange, enemy.raycastMask) && hit.collider.CompareTag ("Player")){ 
			enemy.chaseTarget = hit.transform;

			//when enemy hits player on raycast, update last known position
			enemy.lastKnownPosition = hit.transform.position;

		} else {
			ToAlertState ();
		}
	}


	void Chase(){

		if (enemy.chaseTarget) {
			enemy.navMeshAgent.destination = enemy.chaseTarget.position;
			//enemy.navMeshAgent.Resume ();
			enemy.navMeshAgent.isStopped = false;
		}

		//if enemy's distance is less or equal to it's attack range and it's melee only
		if (enemy.navMeshAgent.remainingDistance <= enemy.attackRange && enemy.onlyMelee == true) {

			//attack!
			enemy.navMeshAgent.Stop ();
			ToAttackState (); 				

		//else if enemy's distance is less or equal to it's shoot range and enemy is not melee only
		} else if(enemy.navMeshAgent.remainingDistance <= enemy.shootRange && enemy.onlyMelee == false){

			//attack!
			enemy.navMeshAgent.Stop ();
			ToAttackState ();
		}
	}

	public void OnTriggerEnter(Collision enemy){
	}

	public void ToPatrolState (){
		Debug.Log ("I should not be able to do this");

	}

	public void ToAttackState(){
		Debug.Log ("Attacks the player!");
		enemy.currentState = enemy.attackState;
	}

	public void ToAlertState(){

		Debug.Log ("I lost the player from my eyes!");
		enemy.currentState = enemy.alertState;
	}


	public void ToChaseState(){
		Debug.Log ("I should not be able to do this");
	}

}
