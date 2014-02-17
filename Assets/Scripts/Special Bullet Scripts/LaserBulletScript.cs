using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class LaserBulletScript : MonoBehaviour {
	
	public LineRenderer line;
	public Vector3 startPos;
	public Vector3 endPos;
	public float width;
	public BulletScript bs;
	public Transform target;
	public float disturbanceFactor;
	int segments;
	Ray laser;
	RaycastHit hit;
	HealthScript th;

	// Use this for initialization
	void Start () {

		line = GetComponent<LineRenderer>();
		bs = GetComponent<BulletScript>();
		line.SetWidth(width,width);

		float inaccuracy = bs.parentUnit.GetComponent<TurretAI>().inaccuracy/1000;
		Vector3 dirOffset = new Vector3 (Random.Range (-inaccuracy,inaccuracy),Random.Range (-inaccuracy,inaccuracy),Random.Range (-inaccuracy,inaccuracy));
		Debug.Log (dirOffset);
		laser = new Ray(transform.position, transform.forward + dirOffset);
		if (Physics.Raycast (laser, out hit, Mathf.Infinity)) {
			target = hit.transform;
			th = target.GetComponent<HealthScript>();
			if (th) {
				th.TakeDamage (bs.damage, bs.apFactor );
			}
		}else{
			target = null;
			th = null;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (target) {
			startPos = transform.position;
			endPos = hit.point;

			line.SetPosition(0,startPos);
			line.SetPosition(1,endPos);
		}
	}

	void OnDrawGizmos () {
		Gizmos.DrawRay (laser);
	}
}
