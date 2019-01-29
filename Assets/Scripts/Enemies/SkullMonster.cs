using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

	SkullMonster.cs

	Controlling behavior for SkullMonster

	Author: Gabe 
	Original Content: Fabryka Twórców Gier
	Based on tutorial: https://youtu.be/799W_Hz68rw

 */

public class SkullMonster : Enemy {

	public void PistolHit(int damage){
	/*

		React script with any hit from raycast in Pisol.cs
		Runs any action to happen when we shoot enemy

	*/
		//print ("We hit SkullMonster! " + damage);
		health = health - damage;
	}
}
