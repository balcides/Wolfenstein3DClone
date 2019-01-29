using UnityEngine;
using System.Collections;

/*

	HeadBobbing.cs

	Not sure the original author but was posted by Fabryka Twórców Gier in his YouTube link
	https://www.youtube.com/watch?v=u7MH76vquEM
	
	And from this Dropbox: 
	https://www.dropbox.com/sh/qk16esy2vkalrwh/AADEyTzRACtCEV2S47W7K36ja/Skrypty/Odcinek%203?dl=0&preview=HeadBobbing.cs&subfolder_nav_tracking=1

	A special thanks to whomever authored the original script. 
	Slightly modified from it's original format for dev purposes. Notation added


 */

public class HeadBobbing : MonoBehaviour
{

	public float bobbingSpeed = 0.18f;
	public float bobbingHeight = 0.2f;
	public float midpoint = 1.8f;
	public bool isHeadBobbing = true;
	public bool enableHeadBob = true;


	//private
	private float timer = 0.0f;

	void Update()
	{

		//enables headbob
		if (enableHeadBob) {	HeadBob ();		}			//note: I miss working on the last of us

	}


	public void HeadBob(){
	/*

		Creates Headbob motion from sinewive inspired by classic FPS movement

 	*/

		float waveslice = 0.0f;
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		//check the position of our camera
		Vector3 cSharpConversion = transform.localPosition;

		//check if player has pressed any buttons
		if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0) {
			
			//if not, set timer to 0
			timer = 0.0f;

		} else {
			
			//otherwise, create sine wave
			waveslice = Mathf.Sin(timer);

			//add bobbing speed to our timer every frame
			timer = timer + bobbingSpeed;

			//if the timer goes beyond 2PI we set it back to 0 and everything starts from the beginning
			if (timer > Mathf.PI * 2) {
				
				timer = timer - (Mathf.PI * 2);
			}
		}

		//if the waveslice is not 0, it means we are moving
		if (waveslice != 0) {
			
			float translateChange = waveslice * bobbingHeight;
			float totalAxes = Mathf.Abs (horizontal) + Mathf.Abs (vertical);

			//according to sine wave we are calculating the camera's position change
			totalAxes = Mathf.Clamp (totalAxes, 0.0f, 1.0f);
			translateChange = totalAxes * translateChange;

			//if headbobbing is happening
			if (isHeadBobbing) {
				
				//and adding this to change the camera's midpoint
				cSharpConversion.y = midpoint + translateChange;
			
				//if not, move to x (side to side)
			} else if (!isHeadBobbing) {
				cSharpConversion.x = translateChange;
			}

		} else {

			if (isHeadBobbing) {
				//otherwise, waveslice is 0, which means we are not moving, so our caemera's position in y is equal to midpoint
				cSharpConversion.y = midpoint;

			//same handling of waveslice side to side
			} else if (!isHeadBobbing) {
				cSharpConversion.x = 0;
			}
		}

		//and at the end, set main camera's position to the new position
		transform.localPosition = cSharpConversion;
	}





}