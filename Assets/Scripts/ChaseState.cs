using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : EnemyAI{

	EnemyStates enemy;

	//contructor for enemy
	public ChaseState(EnemyStates enemy){
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
