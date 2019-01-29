using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStates : MonoBehaviour {

	public Transform[] waypoints;	//for patrol
	public int patrolRange;
	public int shootRange;
	public int attackRange = 1;
	public Transform vision;
	public float stayAlertTime;

	public GameObject missle;
	public float missileDamage;
	public float missileSpeed;

	public bool onlyMelee = false;
	public float meleeDamage;
	public float attackDelay;

	public LayerMask raycastMask;


	[HideInInspector] public AlertState alertState;
	[HideInInspector] public AttackState attackState;
	[HideInInspector] public ChaseState chaseState;
	[HideInInspector] public PatrolState patrolState;
	[HideInInspector] public EnemyAI currentState;
	[HideInInspector] public NavMeshAgent navMeshAgent;
	[HideInInspector] public Vector3 lastKnownPosition;
	[HideInInspector] public Transform chaseTarget = null;



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

		Debug.Log ("Enemy State = " + currentState);
		
	}

	void OnTriggerEnter(Collision otherObj){
		
		//calls trigger state from main EnemyAI interface
		currentState.OnTriggerEnter (otherObj);
	}

	void HiddenShot(Vector3 shotPosition){
		Debug.Log ("someone is shot");
		lastKnownPosition = shotPosition;
		currentState = alertState;
		
	}
}
