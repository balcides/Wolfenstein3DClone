using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*

	Enemy.cs

	Script for Enemy behavior and manager (Base Class). Common functions and variables for all the enemies.

	Author: Gabe 
	Original Content: Fabryka Twórców Gier
	Based on tutorial: https://youtu.be/799W_Hz68rw

 */


public class Enemy : MonoBehaviour {

	public int health;
	public bool canMelleeAttack;
	public bool canShoot;
	public float meleeDamage;
	public float shootDamage;


	public void PistolHit(int damage){
	/*

		React script with any hit from raycast in Pisol.cs
		Runs any action to happen when we shoot enemy

	*/
		Debug.Log("I got hit! " + damage);
	}

}
