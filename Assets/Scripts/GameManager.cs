using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

/// <summary>
/// 
/// Game manager.
/// 
/// Manages main rules of the game overall such as tracking points, win, lose, etc. 
/// 
/// </summary>

public class GameManager : MonoBehaviour {

	//Asset
	private GameObject deathScreen;

	//script
	AssetManager AM;


	//Class setup
	private static GameManager _instance;
	public static GameManager Instance {
		get{  return _instance; }
	}
		
	void Awake(){
		
		AM = GetComponent<AssetManager> ();

		//prevents a duplicate of _instance GameManager script so this is the only one running
		if (_instance == null) { _instance = this;
			
		}else if(_instance != this){ 
			Destroy (gameObject);
			return;
		}
		DontDestroyOnLoad (this);
		InitGame ();

	}

	private void Start(){
		deathScreen = transform.Find ("DeathScreen").gameObject;
	}


	void InitGame(){
		
		//taken from PlayerMovement
		//Cursor.lockState = CursorLockMode.Locked;
		//Cursor.visible = false;
	}


	public void PlayerDeath(){
		
		deathScreen.SetActive (true);
		GameObject player = GameObject.FindGameObjectWithTag ("Player").gameObject;
		player.GetComponent<PlayerMovement> ().enabled = false;
		player.GetComponent<PlayerHealth> ().enabled = false;

		foreach (Transform child in player.transform) {
			if (child.tag != "MainCamera") {
				child.gameObject.SetActive (false);
			}
		}
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		player.tag = "Untagged";

	}


		
}
