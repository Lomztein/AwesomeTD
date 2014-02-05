using UnityEngine;
using System.Collections;

public class MouseScript : MonoBehaviour {

	public GameObject[] stands;
	public GameObject[] turrets;
	public GameObject focusTurret;
	public bool canPlace = true;
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetButtonDown("Fire2")) {
			TestPosition ();
		}

	}

	void TestPosition () {

		TestArea (0.5f);
		if (focusTurret) {
			focusTurret.SendMessage("GetTurretData",GetTurret());
		}else{
			PlaceStand ();
		}
	}

	GameObject GetTurret () {
		GameObject newTurret = turrets[Random.Range(0,turrets.Length)];
		return newTurret;
	}
	
	void PlaceStand () {

		GameObject newStand = stands[Random.Range(0,stands.Length)];
		float newSize = newStand.GetComponent<TurretAI>().size;

		Debug.Log (newSize);
		if (TestArea(newSize)) {
			Instantiate(newStand,transform.position,Quaternion.identity);
		}
	}

	bool TestArea (float size) {
		bool isFree = true;
		Collider[] cols = Physics.OverlapSphere(transform.position,size);
		foreach (Collider c in cols) {
			if (c.gameObject.tag == "Freindly") {
				focusTurret = c.gameObject;
				isFree = false;
			}
			if (c.gameObject.tag == "Enemy") {
				isFree = false;
			}
		}
		return isFree;
	}
}