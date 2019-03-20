using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {


	[HideInInspector]public AudioClip explosionSound;
	AudioSource source;




	// Use this for initialization
	private void Awake () {
		source = GetComponent<AudioSource> ();
	}

	private void Start(){
		source.PlayOneShot (explosionSound);
	}

}
