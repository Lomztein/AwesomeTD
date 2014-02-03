using UnityEngine;
using System.Collections;

public class EnemyInfantryAI : MonoBehaviour {

	public float maxSpeed;
	public Transform pointer;
	public Transform target;
	float speedY;
	Quaternion newLerp;

	CharacterController cc;

	// Use this for initialization
	void Start () {

		cc = GetComponent<CharacterController>();
		target = GameObject.FindGameObjectWithTag("Core").transform;
		pointer = transform.FindChild("Pointer");
		newLerp = transform.rotation;
	
	}
	
	// Update is called once per frame
	void Update () {

		Quaternion newRot = new Quaternion(transform.rotation.x,pointer.rotation.y,transform.rotation.z,pointer.rotation.w);
		newLerp = Quaternion.Lerp (newLerp,newRot,5*Time.deltaTime);
		transform.rotation = newLerp;

		float forwardSpeed = maxSpeed;
		
		speedY += Physics.gravity.y * Time.deltaTime;
		
		Vector3 speed = new Vector3 ( 0, speedY, forwardSpeed);
		
		if (target) {
			pointer.LookAt (target.position);
			speed = transform.rotation * speed;
			cc.Move(speed * Time.deltaTime);
		}
	}
}
