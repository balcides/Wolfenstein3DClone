using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour {

	public List<Transform> weapons;
	public int initialWeapon;
	int selectedWeapon;
	public bool autoFill;

	void Awake(){
		if (autoFill) {
			weapons.Clear ();
			foreach (Transform weapon in transform) {
				weapons.Add (weapon);
			}
		}
	}

	// Use this for initialization
	void Start () {

		selectedWeapon = initialWeapon % weapons.Count;
		UpdateWeapon ();
		
	}
	
	// Update is called once per frame
	void Update () {
		//scroll, aby zimienc bron
		if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
			selectedWeapon = (selectedWeapon + 1) % weapons.Count;
		}

		if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
			selectedWeapon = Mathf.Abs (selectedWeapon - 1) % weapons.Count;
		}

		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			selectedWeapon = 0;
		}

		if(Input.GetKeyDown(KeyCode.Alpha2) && weapons.Count > 1){
			selectedWeapon = 1;	
		}

		UpdateWeapon ();
	}

	void UpdateWeapon(){

		for (int i = 0; i < weapons.Count; i++) {
			if (i == selectedWeapon) {
				weapons [i].gameObject.SetActive (true);
			} else {
				weapons [i].gameObject.SetActive (false);
			}
		}
	}
}
