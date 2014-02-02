using UnityEngine;
using System.Collections;

public class TurretAI : MonoBehaviour {

	public string faction;
	public GameObject turret;
	public Transform pointer;
	public Transform target;
	public int classType;
	public float turnSpeed;
	public float range;
	public Transform model;
	Vector3 targetPos;
	Collider[] nearbyColliders;
	GameObject[] nearbyEnemies;

	Transform[] muzzles;
	public GameObject bulletType;
	public float damage;
	public float apFactor;
	public float inaccuracy;
	public float firerate;
	public float bulletForce;
	public int amount;
	public bool reloaded = true;
	
	// Use this for initialization
	void Start () {

		SearchRandom ();

		model = transform.FindChild ("Turret").FindChild ("Model");
		int index = 0;
		muzzles = new Transform[model.childCount];
		foreach (Transform child in model) {
			muzzles[index] = child;
			index++;
		}
	}

	void FixedUpdate () {
		nearbyColliders = Physics.OverlapSphere(transform.position,range);

		if (target) {
			if (Vector3.Distance (transform.position,target.position) > range) {
				target = null;
			}
		}
		int nEnemies = 0;
		
		foreach (Collider c in nearbyColliders) {
			if (c.gameObject.tag == "Enemy") {
				nEnemies++;
			}
		}
		
		nearbyEnemies = new GameObject[nEnemies];
		
		int index = 0;
		foreach (Collider c in nearbyColliders) {
			if (c.gameObject.tag == "Enemy") {
				nearbyEnemies[index] = c.gameObject;
				index++;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (target) {
			targetPos = target.position;
			Ray ray = new Ray (pointer.position,pointer.forward);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, range)) {
				if (hit.transform.tag == "Enemy") {
					Fire ();
				}
			}
		}else{
			if (nearbyEnemies.Length != 0) {
				FindClosest ();
			}
		}

		pointer.LookAt(targetPos);
		turret.transform.rotation = Quaternion.Lerp (turret.transform.rotation,pointer.rotation,turnSpeed * Time.deltaTime);
	}

	void FindClosest () {

		GameObject nearestObj = null;

		if (nearbyEnemies.Length > 0) {
			
			float nearestDistanceSqr = Mathf.Infinity;
			GameObject[] taggedGameObjects = nearbyEnemies;
			nearestObj = null;
			
			// loop through each tagged object, remembering nearest one found
			if (taggedGameObjects[0] != null) {
				foreach (GameObject obj in taggedGameObjects) {
					
					Vector3 objectPos = obj.transform.position;
					float distanceSqr = (objectPos - transform.position).sqrMagnitude;
					
					if (distanceSqr < nearestDistanceSqr) {
						nearestObj = obj;
						nearestDistanceSqr = distanceSqr;
					}
				}
			}
		}
		if (nearestObj) {
			target = nearestObj.transform;
		}
	}

	void Fire () {

		if (reloaded == true) {
			reloaded = false;
			Invoke ("Reload",firerate);
			foreach (Transform m in muzzles) {
				int intAmount = amount;
				while (intAmount > 0) {
					intAmount--;
					GameObject newBullet = (GameObject)Instantiate(bulletType,m.position,m.rotation);
					BulletScript ns = newBullet.GetComponent<BulletScript>();
					newBullet.rigidbody.AddForce (m.forward * bulletForce);
					newBullet.rigidbody.AddForce (m.up * inaccuracy);
					newBullet.rigidbody.AddForce (m.right * inaccuracy);
					ns.damage = damage;
					ns.apFactor = apFactor;
					ns.faction = faction;
				}
			}
		}
	}

	void Reload () {
		reloaded = true;
	}

	void OnDrawGizmos () {
		Gizmos.DrawWireSphere(transform.position,range);
	}

	void SearchRandom () {
		Invoke ("SearchRandom",Random.Range (2f,5f));
		if (!target) {
			Vector3 newPos = new Vector3 (Random.Range (-1f,1f),0,Random.Range (-1f,1f));
			targetPos = pointer.position + newPos;
		}
	}
}
