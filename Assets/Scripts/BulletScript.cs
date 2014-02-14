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

	void Start () {
		Destroy(gameObject,life);
	}

	void OnTriggerEnter (Collider other) {
		
		oh = other.gameObject.GetComponent<HealthScript>();
		if (oh) {
			if (oh.faction != faction) {
				Destroy (gameObject);
				oh.TakeDamage(damage,apFactor);
				if (hitParticle) {
					Instantiate(hitParticle,transform.position,Quaternion.identity);
				}
			}
		}

		if (other.tag == "Terrain") {
			Destroy (gameObject);
			if (hitParticle) {
				Instantiate(hitParticle,transform.position,Quaternion.identity);
			}
		}
	}
}