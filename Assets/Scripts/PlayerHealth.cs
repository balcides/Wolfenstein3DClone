using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

	public int maxHealth;
	public int maxArmor;
	public AudioClip hit;
	public FlashScreen flash;
	AudioSource source;
	bool isGameOver = false;
	[SerializeField] float armor;
	[SerializeField] float health;


	// Use this for initialization
	void Start () {

		armor = 0;
		health = maxHealth;
		source = GetComponent<AudioSource> ();
		
	}

	private void Update(){
		armor = Mathf.Clamp (armor, 0, maxArmor);
		health = Mathf.Clamp (health, -Mathf.Infinity, maxHealth);

		if (health <= 0 && !isGameOver) {
			print ("gameover");
			isGameOver = true;
			GameManager.Instance.PlayerDeath ();
		}

	}

	public void AddHealth(float value){
		health += value;
	}

	public void AddArmor(float value){
		armor += value;

	}

	void EnemyHit(float damage){

		Debug.Log ("Enemy hit happening");
		if (armor > 0 && armor >= damage) {
			armor -= damage;

		} else if (armor > 0 && armor < damage) {
			damage -= armor;
			armor = 0;
			health -= damage;

		} else {
			health -= damage;
		}

		source.PlayOneShot (hit);
		health -= damage;
		flash.TookDamage ();
	}
}
