using UnityEngine;
using System.Collections;

public class MouseScript : MonoBehaviour {

	public GameObject[] stands;
	public GameObject[] turrets;
	public GameObject[] totalIndex;
	public GameObject focusTurret;
	public int selectedIndex;
	public bool showMenu = false;
	public int menuID;
	public bool canPlace = true;

	void Start () {

		totalIndex = new GameObject[stands.Length + turrets.Length];
		for (int i = 0;i<totalIndex.Length;i++) {
			if (i < turrets.Length) {
				totalIndex[i] = stands[i];
			}else{
				totalIndex[i] = turrets[i-stands.Length];
			}
		}
	}
	
	void Update () {
		
		if (Input.GetButtonDown("Fire2")) {
			TestPosition ();
		}

		float size = totalIndex[selectedIndex].GetComponent<TurretAI>().size * 2;
		Vector3 newSize = new Vector3 (size,0.5f,size);
		transform.localScale = newSize;

	}

	void TestPosition () {

		TestArea (0.5f);
		if (focusTurret) {
			if (focusTurret.GetComponent<TurretAI>().turret.transform.childCount == 0) {
				focusTurret.SendMessage("GetTurretData",GetTurret());
				focusTurret = null;
			}
		}else{
			PlaceStand ();
		}
	}

	GameObject GetTurret () {
		GameObject newTurret = turrets[Random.Range(0,turrets.Length)];
		return newTurret;
	}
	
	void PlaceStand () {

		GameObject newStand = stands[selectedIndex];
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