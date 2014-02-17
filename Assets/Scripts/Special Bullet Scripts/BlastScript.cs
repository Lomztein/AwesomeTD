using UnityEngine;
using System.Collections;

public class BlastScript : MonoBehaviour {

	public BulletScript bs;
	public float growSpeed;
	public GameObject model;
	float startTime;
	float time;
	float range;

	// Use this for initialization
	void Start () {

		bs = GetComponent<BulletScript>();
		model = transform.FindChild("Model").gameObject;
		range = bs.range;
		startTime = range/15 * 0.5f;
		time = startTime;
		Destroy (gameObject, startTime);
		
	}

	void Update () {
		time -= Time.deltaTime;
		transform.localScale += new Vector3 (growSpeed,growSpeed,growSpeed) * Time.deltaTime;
		model.renderer.material.color = new Color (model.renderer.material.color.r,model.renderer.material.color.g,model.renderer.material.color.b,time/startTime);
	}
	
	void OnTriggerStay (Collider col) {
		GameObject other = col.gameObject;
		if (other.tag != bs.faction) {
			HealthScript oh = other.GetComponent<HealthScript>();
			if (oh) {
				oh.TakeDamage (bs.damage * Time.deltaTime, bs.apFactor);
			}
		}
	}
}
