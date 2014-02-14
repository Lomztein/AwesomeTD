using UnityEngine;
using System.Collections;

public class BlastScript : MonoBehaviour {

	public BulletScript bs;
	public float growSpeed;
	public GameObject model;
	float time;

	// Use this for initialization
	void Start () {

		bs = GetComponent<BulletScript>();
		model = transform.FindChild("Model").gameObject;
		time = bs.life;
	
	}

	void Update () {
		time -= Time.deltaTime;
		transform.localScale += new Vector3 (growSpeed,growSpeed,growSpeed) * Time.deltaTime;
		model.renderer.material.color = new Color (model.renderer.material.color.r,model.renderer.material.color.g,model.renderer.material.color.b,time/bs.life);
	}
	
	void OnTriggerStay (Collider col) {
		GameObject other = col.gameObject;
		if (other.tag != bs.faction) {
			HealthScript oh = other.GetComponent<HealthScript>();
			if (oh) {
				oh.TakeDamage (bs.damage * Time.deltaTime, bs.apFactor);
				Debug.Log ("Did " + bs.damage * Time.deltaTime + " damage to " + other.name);
			}
		}
	}
}
