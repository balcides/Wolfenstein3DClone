using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStates : MonoBehaviour {

	public Transform[] waypoints;	//for patrol
	public int patrolRange;

	[HideInInspector] public AlertState alertState;
	[HideInInspector] public AttackState attackState;
	[HideInInspector] public ChaseState chaseState;
	[HideInInspector] public PatrolState patrolState;
	[HideInInspector] public EnemyAI currentState;
	[HideInInspector] public NavMeshAgent navMeshAgent;

	public Transform chaseTarget;



	void Awake(){
		alertState = new AlertState (this);
		attackState = new AttackState (this);
		chaseState = new ChaseState (this);
		patrolState = new PatrolState (this);
		navMeshAgent = GetComponent<NavMeshAgent> ();

	}

	// Use this for initialization
	void Start () {
		currentState = patrolState;
	}
	
	// Update is called once per frame
	void Update () {

		//calls update actions of main enemyAI interface
		currentState.UpdateActions ();
		
	}

	void OnTriggerEnter(Collision otherObj){
		
		//calls trigger state from main EnemyAI interface
		currentState.OnTriggerEnter (otherObj);
	}
}
