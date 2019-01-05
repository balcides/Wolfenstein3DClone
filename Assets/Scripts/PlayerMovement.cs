using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

	PlayerMovement.cs

	Retro FPS style game mechanics for player movement based on a hybrid of Wolfenstein3D and Doom.
	

	Author: Gabe 
	Original Content: Fabryka Twórców Gier
	Based on tutorial: https://youtu.be/799W_Hz68rw

 */

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour {

	public float playerWalkingSpeed = 5f;
	public float playerRunningSpeed = 15f;
	public float jumpStrength = 4f; 
	public float verticalCursorSensitivty = 10f;
	public float horiztonalCursorSensitivty = 1f;
	public float verticalRotationLimit = 80;

	float forwardMovement;
	float sidewaysMovement;
	float verticalVelocity;

	CharacterController cc;
	float verticalRotation = 0;

	void Awake(){
		
		cc = GetComponent<CharacterController> ();
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;

	}


	void Update(){

		CharacterMove ();			//controls character movement and camera
	
	}


	void CharacterMove(){
	/*

		Controls player and camera movement

	*/

		//Look around left and right
		float horizontalRotation = Input.GetAxis("Mouse X") * verticalCursorSensitivty;
		transform.Rotate (0, horizontalRotation, 0);

		//Look around vertical
		verticalRotation -= Input.GetAxis("Mouse Y") * verticalCursorSensitivty ;
		verticalRotation = Mathf.Clamp (verticalRotation, -verticalRotationLimit, verticalRotationLimit);

		Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0,0);

		//movement - works only when player is grounded
		if (cc.isGrounded) {

			//walking
			forwardMovement = Input.GetAxis ("Vertical") * playerWalkingSpeed;
			sidewaysMovement = Input.GetAxis ("Horizontal") * playerWalkingSpeed;

			//if holding shift, running
			if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) {
				forwardMovement = Input.GetAxis ("Vertical") * playerRunningSpeed;
				sidewaysMovement = Input.GetAxis ("Horizontal") * playerRunningSpeed;

			}
		}

		//addg gravity to jump, 
		verticalVelocity += Physics.gravity.y * Time.deltaTime;

		//Jump if button and grounded.
		if (Input.GetButton ("Jump") && cc.isGrounded) {
			verticalVelocity = jumpStrength;
		}

		//move player
		Vector3 playerMovement = new Vector3 (sidewaysMovement, verticalVelocity, forwardMovement);
		cc.Move (transform.rotation * playerMovement * Time.deltaTime);

	}

}
