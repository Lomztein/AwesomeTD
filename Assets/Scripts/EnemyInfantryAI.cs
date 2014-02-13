using UnityEngine;
using System.Collections;

public class EnemyInfantryAI : MonoBehaviour {

	public float maxSpeed;
	public Transform pointer;
	public Transform target;
	float speedY;
	Quaternion newLerp;
	public float damage;
	public float apFactor;
	public float range;

	CharacterController cc;
	Quaternion newRot;
	float forwardSpeed;
	Vector3 speed;
	HealthScript targetHealth;

	// Use this for initialization
	void Start () {

		cc = GetComponent<CharacterController>();
		pointer = transform.FindChild("Pointer");
		newLerp = transform.rotation;
		target = GetTarget ();
	
	}

	Transform GetTarget () {

		Transform newTarget = GameObject.FindGameObjectWithTag("Core").transform;
		targetHealth = newTarget.GetComponent<HealthScript>();
		return newTarget;

	}
	
	// Update is called once per frame
	void Update () {

		if (cc.isGrounded) {

			newRot = new Quaternion(transform.rotation.x,pointer.rotation.y,transform.rotation.z,pointer.rotation.w);
			newLerp = Quaternion.Lerp (newLerp,newRot,5*Time.deltaTime);
			transform.rotation = newLerp;

			forwardSpeed = maxSpeed;
			speedY = 0;

		}else{
			speedY += Physics.gravity.y * Time.deltaTime;
		}

		speed = new Vector3 ( 0, speedY, forwardSpeed);
		speed = transform.rotation * speed;
		
		if (target) {
			pointer.LookAt (target.position);
			cc.Move(speed * Time.deltaTime);
			if (Vector3.Distance(transform.position,target.position) < range) {
				targetHealth.TakeDamage (damage,apFactor);
			}
		}
	}
}
