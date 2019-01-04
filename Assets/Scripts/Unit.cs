using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Unit.cs
/// 
/// Meant as a basic RPG class. Place this on any unit to give it speed, XP, attack, defense, etc. attributes
/// 
/// </summary>

public class Unit : MonoBehaviour {

	//stats
	public string modelname;
	public int level;
	public int hp;
	public int attack;
	public int defense;
	public int speed;
	public int xp;

	//initialize
	public static Unit instance = null;

	void Awake(){
		instance = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
