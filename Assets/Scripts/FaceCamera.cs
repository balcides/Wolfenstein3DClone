using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

	FaceCamera.cs

	Retro FPS style NPC Sprite Manager to make it always face camera.
	Note: assign this script to enemyNPC
	

	Author: Gabe 
	Original Content: Fabryka Twórców Gier
	Based on tutorial: https://youtu.be/799W_Hz68rw

 */

public class FaceCamera : MonoBehaviour {

	Vector3 cameraDirection;

	// Update is called once per frame
	void Update () {

		//NPC spite always faces camera based on lookdirection

		cameraDirection = Camera.main.transform.forward;
		cameraDirection.y = 0;
		transform.rotation = Quaternion.LookRotation (cameraDirection);
	}
}
