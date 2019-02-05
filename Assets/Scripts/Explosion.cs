using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {


	[HideInInspector]public AudioClip explosionSound;
	AudioSource source;

	//only temporarily removed the object after the animation
	float lifespan;



	// Use this for initialization
	private void Awake () {
		source = GetComponent<AudioSource> ();
	}

	private void Start(){
		source.PlayOneShot (explosionSound);
	}
	
	// Update is called once per frame
	void Update () {
		lifespan += Time.deltaTime;
		if (lifespan > 2) {
			Destroy (this.gameObject);
		}
	}
}
