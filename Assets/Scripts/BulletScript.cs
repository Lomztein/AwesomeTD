using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public string faction;
	public float damage;
	public float apFactor;
	public float life = 5;
	public GameObject hitParticle;
	public GameObject parentUnit;
	public float range;
	public Transform target;
	public HealthScript oh;
	public bool destroyOnHit = true;
	public bool doNativeDamage = true;

	void Start () {
		Destroy(gameObject,life);
	}

	void OnTriggerEnter (Collider other) {
		
		oh = other.gameObject.GetComponent<HealthScript>();
		if (oh) {
			if (oh.faction != faction) {
				if (destroyOnHit) { Destroy (gameObject); }
				if (doNativeDamage) { oh.TakeDamage(damage,apFactor); }
				if (hitParticle) {
					Instantiate(hitParticle,transform.position,Quaternion.identity);
				}
			}
		}

		if (other.tag == "Terrain") {
			if (destroyOnHit) { Destroy (gameObject); }
			if (hitParticle) {
				Instantiate(hitParticle,transform.position,Quaternion.identity);
			}
		}
	}
}