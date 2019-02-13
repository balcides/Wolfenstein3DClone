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
	public float jumpStrength = 14f; 
	public float verticalCursorSensitivty = 10f;
	public float horiztonalCursorSensitivty = 1f;
	public float verticalRotationLimit = 80;
	public AudioClip pickupSound;
	public FlashScreen flash;

	public bool lockCursor;
	float forwardMovement;
	float sidewaysMovement;
	float verticalVelocity;


	CharacterController cc;
	AudioSource source;
	float verticalRotation = 0;

	void Awake(){

		//locks cursor at start of game
		lockCursor = true;
		cc = GetComponent<CharacterController> ();
		source = GetComponent<AudioSource> ();
		Cursor.visible = false;

		if (lockCursor) {
			Cursor.lockState = CursorLockMode.Locked;
		}

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

				//if player is using move keys while running
				if (Input.GetAxis ("Vertical") != 0 || Input.GetAxis ("Horizontal") != 0) {

					//if shift is being pressed during run and move, use walk spread for crosshair
					if (Input.GetKey (KeyCode.LeftShift)) {
						DynamicCrosshair.spread = DynamicCrosshair.WALK_SPREAD;
					}
				}

			} else {
					
				//TODO: Depricate
				//crosshair works when jumping and not grounded	(messes things up. depricate)
				//DynamicCrosshair.spread = DynamicCrosshair.JUMP_SPREAD;
			}

			//check if any direction when landing, for crosshair behavior
			if (Input.GetAxis ("Vertical") != 0 || Input.GetAxis ("Horizontal") != 0) {

				//crosshair when running
				if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
					DynamicCrosshair.spread = DynamicCrosshair.RUN_SPREAD;

				} else {
					//crosshair spread when walking
					DynamicCrosshair.spread = DynamicCrosshair.WALK_SPREAD;
				}
			}

		//else jumping
		} else {
			
			//if jumping, use crosshair spread value
			DynamicCrosshair.spread = DynamicCrosshair.JUMP_SPREAD;
		}

		//add gravity to jump, 
		verticalVelocity += Physics.gravity.y * Time.deltaTime;

		//Jump if button and grounded.
		if (Input.GetButton ("Jump") && cc.isGrounded) {
			verticalVelocity = jumpStrength;
		}

		//move player
		Vector3 playerMovement = new Vector3 (sidewaysMovement, verticalVelocity, forwardMovement);
		cc.Move (transform.rotation * playerMovement * Time.deltaTime);

	}

	private void OnTriggerEnter(Collider other){
		Debug.Log ("I'm hitting something...other=" + other);

		if (other.CompareTag ("HpBonus")) {
			GetComponent<PlayerHealth> ().AddHealth (20);

		} else if (other.CompareTag ("ArmorBonus")) {
			Debug.Log ("got armor");
			GetComponent<PlayerHealth> ().AddArmor (50);

		} else if (other.CompareTag ("AmmoBonus")) {
			transform.Find ("Weapons").Find ("PistolHand").GetComponent<Pistol> ().AddAmmo (15);
		}

		if (other.CompareTag ("HpBonus") || other.CompareTag ("ArmorBonus") || other.CompareTag ("AmmoBonus")) {
			flash.PickedUpBonus ();
			source.PlayOneShot (pickupSound);
			Destroy (other.gameObject);

		}
	}

}
