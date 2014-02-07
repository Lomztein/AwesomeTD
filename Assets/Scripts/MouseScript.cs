using UnityEngine;
using System.Collections;

public class MouseScript : MonoBehaviour {

	public GameObject[] stands;
	public GameObject[] turrets;
	public GameObject[] totalIndex;
	public int[] costs;
	public GameObject focusTurret;
	public int selectedIndex;
	public bool showMenu = false;
	public int menuID;
	public bool canPlace = true;
	StatsManager stats;

	void Start () {

		stats = GameObject.FindGameObjectWithTag("Stats").GetComponent<StatsManager>();
		totalIndex = new GameObject[stands.Length + turrets.Length + 1];
		for (int i = 0;i<totalIndex.Length-1;i++) {
			if (i < stands.Length) {
				totalIndex[i + 1] = stands[i];
			}else{
				totalIndex[i + 1] = turrets[i-stands.Length];
			}
		}

		costs = new int[totalIndex.Length-1];
		for (int i = 0;i<totalIndex.Length-1;i++) {
			if (i <= stands.Length) {
				costs[i] = totalIndex[i+1].GetComponent<TurretAI>().cost;
			}else{
				costs[i] = totalIndex[i+1].GetComponent<TurretData>().cost;
			}
		}
	}
	
	void Update () {
		
		if (Input.GetButtonDown("Fire2")) {
			TestPosition ();
			focusTurret = null;
		}

		float size = 1;
		if (totalIndex[selectedIndex] && selectedIndex <= stands.Length) {
			size = totalIndex[selectedIndex].GetComponent<TurretAI>().size * 2;
		}
		Vector3 newSize = new Vector3 (size,0.5f,size);
		transform.localScale = newSize;

//		Debug.Log (selectedIndex);

	}

	void TestPosition () {

		TestArea (0.5f);
		if (focusTurret) {
			if (selectedIndex > stands.Length) {
				GameObject t = GetTurret ();
				TurretAI ai = focusTurret.GetComponent<TurretAI>();
				TurretData td = t.GetComponent<TurretData>();
				Debug.Log (ai.classType + ", " + td.classType);
				if (ai.turret.transform.childCount == 0 && td.classType <= ai.classType && td.cost <= stats.credits) {
					focusTurret.SendMessage("GetTurretData",t);
					stats.credits -= td.cost;
					focusTurret = null;
				}
			}
		}else{
			PlaceStand ();
		}
	}

	GameObject GetTurret () {
		GameObject newTurret = totalIndex[selectedIndex];
		return newTurret;
	}
	
	void PlaceStand () {

		if (selectedIndex <= stands.Length) {
			GameObject newStand = stands[selectedIndex-1];
			TurretAI ai = newStand.GetComponent<TurretAI>();
			float newSize = ai.size;

			//Debug.Log (newSize);
			if (TestArea(newSize) && ai.cost <= stats.credits) {
				Instantiate(newStand,transform.position,Quaternion.identity);
				stats.credits -= ai.cost;
			}
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