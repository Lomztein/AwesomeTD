using UnityEngine;
using System.Collections;

public class TurretAI : MonoBehaviour {

	public string standName;
	public string standDiscription;

	public string faction;
	public GameObject turret;
	public Transform pointer;
	public Transform target;
	public int classType;
	public float turnSpeed;
	public float range;
	public Transform model;
	public float size = 0.5f;
	Vector3 targetPos;
	Collider[] nearbyColliders;
	GameObject[] nearbyEnemies;
	CharacterController targetC;
	public bool reloaded = true;
	public Transform[] muzzles;
	public int cost;
	int muzzleIndex = 0;

	GameObject bulletType;
	GameObject fireParticle;
	float damage;
	float apFactor;
	float inaccuracy;
	float firerate;
	float bulletForce;
	int amount;
	float sequenceTime;

	float bulletVel;
	
	// Use this for initialization
	void Start () {

		SearchRandom ();
		size = GetComponent<SphereCollider>().radius;
		if (turret.transform.childCount > 0) {
			GetTurretData (turret.transform.GetChild (0).gameObject);
		}

	}

	public void GetTurretData (GameObject newTurret) {

		TurretData newData = newTurret.GetComponent<TurretData>();
		bulletType = newData.bulletType;
		fireParticle = newData.fireParticle;
		damage = newData.damage;
		apFactor = newData.apFactor;
		inaccuracy = newData.inaccuracy;
		firerate = newData.firerate;
		bulletForce = newData.bulletForce;
		amount = newData.amount;
		sequenceTime = newData.sequenceTime;
		range *= newData.rangeMultiplier;
		Vector3 newRot = newData.turretAngleOffset;

		bulletVel = (bulletForce/bulletType.rigidbody.mass)*Time.fixedDeltaTime;


		GameObject nt = null;

		if (turret.transform.childCount == 0) {
			nt = (GameObject)Instantiate (newTurret,turret.transform.position,Quaternion.Euler (newRot));
			nt.transform.parent = turret.transform;
		}

		if (nt) {
			model = nt.transform;
		}else{
			model = newTurret.transform;
		}

		int index = 0;
		muzzles = new Transform[model.childCount-1];
		foreach (Transform child in model) {
			if (child.name == "Muzzle") {
//				Debug.Log (child.name);
				muzzles[index] = child;
				index++;
			}
		}
		
		float y = 0;
		foreach (Transform m in muzzles) {
			y += m.position.y;
			y /= (model.childCount-1);
		}

		pointer.position = new Vector3 (turret.transform.position.x,y,turret.transform.position.z);
		
	}


	void FixedUpdate () {

			if (model) {

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
	}
	
	// Update is called once per frame
	void Update () {

		if (model) {

			if (target) {
				targetPos = CalculateFuturePosition(target.position,targetC.velocity,bulletVel);
				Fire ();
			}else{
				if (nearbyEnemies.Length != 0) {
					FindClosest ();
				}
			}

			pointer.LookAt(targetPos);
			turret.transform.rotation = Quaternion.Lerp (turret.transform.rotation,pointer.rotation,turnSpeed * Time.deltaTime);

		}
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

					if (obj) {
						Vector3 objectPos = obj.transform.position;
						float distanceSqr = (objectPos - transform.position).sqrMagnitude;
						
						if (distanceSqr < nearestDistanceSqr) {
							nearestObj = obj;
							nearestDistanceSqr = distanceSqr;
						}
					}
				}
			}
		}
		if (nearestObj) {
			target = nearestObj.transform;
			targetC = target.GetComponent<CharacterController>();
		}
	}

	void Fire () {

		if (reloaded == true) {
			muzzleIndex = 0;
			reloaded = false;
			Invoke ("Reload",firerate);
			if (muzzleIndex+1 != muzzles.Length) {
				Invoke ("FireSequence",sequenceTime);
			}
			int intAmount = amount;
			while (intAmount > 0) {
				intAmount--;
				GameObject newBullet = (GameObject)Instantiate(bulletType,muzzles[muzzleIndex].position,muzzles[muzzleIndex].rotation);
				Instantiate(fireParticle,muzzles[muzzleIndex].position,muzzles[muzzleIndex].rotation);
				FeedBulletData (newBullet);
			}
			muzzleIndex++;
		}
	}

	void FireSequence () {
		if (muzzleIndex+1 != muzzles.Length) {
			Invoke ("FireSequence",sequenceTime);
		}
		int intAmount = amount;
		while (intAmount > 0) {
			intAmount--;
			GameObject newBullet = (GameObject)Instantiate(bulletType,muzzles[muzzleIndex].position,muzzles[muzzleIndex].rotation);
			Instantiate(fireParticle,muzzles[muzzleIndex].position,muzzles[muzzleIndex].rotation);
			FeedBulletData (newBullet);
		}
		muzzleIndex++;
	}

	void Reload () {
		reloaded = true;
	}

	void OnDrawGizmos () {
		Gizmos.DrawWireSphere(transform.position,range);
		if (target) {
			Gizmos.DrawSphere(targetPos,0.25f);
		}
	}

	void SearchRandom () {
		Invoke ("SearchRandom",Random.Range (2f,5f));
		if (!target) {
			Vector3 newPos = new Vector3 (Random.Range (-1f,1f),0,Random.Range (-1f,1f));
			targetPos = pointer.position + newPos;
		}
	}

	Vector3 CalculateFuturePosition (Vector3 pos, Vector3 velT, float bs) {
		float distance = Vector3.Distance (muzzles[0].position,pos);
		float time = distance/bs;
		Vector3 fp = pos + velT * time;
		return fp;
	}

	void FeedBulletData (GameObject newBullet) {
		BulletScript ns = newBullet.GetComponent<BulletScript>();
		newBullet.rigidbody.AddForce (muzzles[muzzleIndex].forward * bulletForce * Random.Range(0.9f,1.1f));
		newBullet.rigidbody.AddForce (muzzles[muzzleIndex].up * Random.Range(-inaccuracy,inaccuracy));
		newBullet.rigidbody.AddForce (muzzles[muzzleIndex].right * Random.Range(-inaccuracy,inaccuracy));
		ns.damage = damage;
		ns.apFactor = apFactor;
		ns.faction = faction;
		ns.range = range;
		ns.parentUnit = gameObject;
	}
}
