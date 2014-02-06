using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

	public string faction;
	public float maxHealth;
	public float maxArmor;
	public float maxRegen;
	public float health;
	public float armor;
	public float regenSpeed;
	public GameObject debris;

	// Use this for initialization
	void Start () {

		if (health == 0) {
			health = maxHealth;
		}
	
	}
	
	// Update is called once per frame
	void Update () {

		if (health < maxRegen) {
			health += regenSpeed * Time.deltaTime;
		}

		if (health > maxHealth) {
			health = maxHealth;
		}

		if (armor > maxArmor) {
			armor = maxArmor;
		}

		if (health < 0) {
			Destroy(gameObject);
			if (debris) {
				Instantiate(debris,transform.position,Quaternion.identity);
			}
		}
	}

	public void TakeDamage (float d, float a) {

		if (armor > 0) {
			armor -= d*a;
		}else{
			health -= d;
		}
	}
}
