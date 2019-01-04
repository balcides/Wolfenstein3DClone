using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// GameMechanics.cs
/// 
/// Manages common elements of gameplay to keep Game Manager about rules and consequences
/// while mechanics deals with common things like explosion on destroy for all units and things like that. Common game elements per units
/// 
/// note: you may want to attach this to camera alongside the GameManager.cs
/// 
/// </summary>

public class GameMechanics : MonoBehaviour {


	//scripts
	AssetManager AM;
	GameManager GM;

	void Awake(){

		AM = GetComponent<AssetManager> ();
		GM = GetComponent<GameManager> ();

	}


	// Use this for initialization
	void Start () {
		
	
	}


	// Update is called once per frame
	void Update () {

	
	}


}
