using UnityEngine;
using System.Collections;

public class MouseScript : MonoBehaviour {
	
	public GameObject[] turrets;
	public bool canPlace = true;
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetButtonDown("Fire1")) {
			PlaceTurret ();
		}

	}
	
	void PlaceTurret () {
		GameObject newTurret = turrets[Random.Range(0,turrets.Length)];
		float newSize = newTurret.GetComponent<TurretAI>().size;

		Debug.Log (newSize);
		if (TestArea(newSize)) {
			Instantiate(newTurret,transform.position,Quaternion.identity);
		}
	}

	bool TestArea (float size) {
		bool isFree = true;
		Collider[] cols = Physics.OverlapSphere(transform.position,size);
		foreach (Collider c in cols) {
			Debug.Log (c.gameObject.tag);
			if (c.gameObject.tag == "Freindly" || c.gameObject.tag == "Enemy") {
				isFree = false;
			}
		}
		return isFree;
	}
}