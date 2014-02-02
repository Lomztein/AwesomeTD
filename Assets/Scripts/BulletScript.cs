using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public string faction;
	public float damage;
	public float apFactor;

	void OnTriggerEnter (Collider other) {

		HealthScript oh = other.gameObject.GetComponent<HealthScript>();
		if (oh) {
			if (oh.faction != faction) {
				Destroy (gameObject);
				oh.TakeDamage(damage,apFactor);
			}
		}
	}
}