using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour {

	Transform player;

	// Use this for initialization
	void Start () {

		player = GameObject.FindGameObjectWithTag ("Player").transform;
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (player);
		
	}
}
