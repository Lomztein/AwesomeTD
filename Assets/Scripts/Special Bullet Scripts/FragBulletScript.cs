using UnityEngine;
using System.Collections;

public class FragBulletScript : MonoBehaviour {

	public int fragments;
	public float damage;
	public float apFactor;
	public float minRange;
	public float maxRange;
	public string faction;

	void OnDestroy () {

		for (int f = 0;f<fragments;f++) {

			Vector3 newDir = new Vector3 (Random.Range (0f,360f),Random.Range (0f,360f),Random.Range (0f,360f));
			Ray newRay = new Ray(transform.position,newDir);
			RaycastHit hit;
			if (Physics.Raycast (newRay, out hit, Random.Range (minRange,maxRange))) {
				GameObject other = hit.transform.gameObject;
				HealthScript oh = other.GetComponent<HealthScript>();
				if (oh) {
					if (oh.faction != faction) {
						oh.TakeDamage (damage,apFactor);
						Debug.Log ("Did damage to " + other.name);
					}
				}
			}
		}
	}
}
