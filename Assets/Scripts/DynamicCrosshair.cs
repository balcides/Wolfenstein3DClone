using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

	DynamicCrosshair.cs

	Retro FPS style crosshair mechanics.
	

	Author: Gabe 
	Original Content: Fabryka Twórców Gier
	Based on tutorial: https://youtu.be/799W_Hz68rw

 */


public class DynamicCrosshair : MonoBehaviour {

	//default is 20 for spread
	static public float spread = 0;
	public const float PISTOL_SHOOTING_SPREAD = 50;
	public const float JUMP_SPREAD = 75;
	public const float RUN_SPREAD = 40;
	public const float WALK_SPREAD = 25;

	public GameObject crosshair;
	GameObject topPart;
	GameObject bottomPart;
	GameObject leftPart;
	GameObject rightPart;

	float initialPosition; 


	void Start(){
		
		topPart = crosshair.transform.Find("topCross").gameObject;
		bottomPart = crosshair.transform.Find("bottomCross").gameObject;
		leftPart = crosshair.transform.Find("leftCross").gameObject;
		rightPart = crosshair.transform.Find("rightCross").gameObject;

		initialPosition = topPart.GetComponent<RectTransform> ().localPosition.y;

	}

	void Update(){

		//check spread so crosshair dynamics only work when moving
		if (spread > 0) {
			
			topPart.GetComponent<RectTransform> ().localPosition = new Vector3 (0, initialPosition + spread, 0);
			bottomPart.GetComponent<RectTransform> ().localPosition = new Vector3 (0, -(initialPosition + spread), 0);
			leftPart.GetComponent<RectTransform> ().localPosition = new Vector3 (-(initialPosition + spread),0 , 0);
			rightPart.GetComponent<RectTransform> ().localPosition = new Vector3 (initialPosition + spread,0 , 0);

			//spread goes over periord of time
			spread -= 100 * Time.deltaTime;
		}
	}


}
