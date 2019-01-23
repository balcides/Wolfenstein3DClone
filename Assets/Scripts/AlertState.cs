using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : EnemyAI {

	EnemyStates enemy;

	//contructor for enemy
	public AlertState(EnemyStates enemy){
		this.enemy = enemy;
	}

	public void UpdateActions(){

	}

	public void OnTriggerEnter(Collision enemy){
	}

	public void ToPatrolState (){
	}

	public void ToAttackState(){
	}

	public void ToAlertState(){
	}

	public void ToChaseState(){
	}
}
