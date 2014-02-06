using UnityEngine;
using System.Collections;

public class TurretData : MonoBehaviour {

	public int classType;
	public string turretName;
	public string turretDescription;
	public int cost;

	public GameObject bulletType;
	public GameObject fireParticle;
	public float damage;
	public float apFactor;
	public float inaccuracy;
	public float firerate;
	public float bulletForce;
	public int amount = 1;
	public float sequenceTime;
	public float rangeMultiplier = 1;
	public Vector3 turretAngleOffset;

}
