using UnityEngine;
using System.Collections;

public class MouseScript : MonoBehaviour {

	public GameObject[] stands;
	public GameObject[] turrets;
	public GameObject[] totalIndex;
	public int[] costs;
	public int selectedStand;
	public int selectedTurret;
	public GameObject focusTurret;
	public int selectedIndex;
	public bool showMenu = false;
	public int menuID;
	public bool canPlace = true;
	public bool showTurretOptions;
	public TurretAI[] ais;
	public TurretData[] tds;
	float size = 1;
	TerrainScript terrain;
	TurretAI focusAI;
	StatsManager stats;

	void Start () {

		terrain = GameObject.FindGameObjectWithTag("Terrain").GetComponent<TerrainScript>();
		stats = GameObject.FindGameObjectWithTag("Stats").GetComponent<StatsManager>();
		totalIndex = new GameObject[stands.Length + turrets.Length + 1];
		for (int i = 0;i<totalIndex.Length-1;i++) {
			if (i < stands.Length) {
				totalIndex[i + 1] = stands[i];
			}else{
				totalIndex[i + 1] = turrets[i-stands.Length];
			}
		}

		ais = new TurretAI[stands.Length];
		for (int i=0;i<stands.Length;i++) {
			ais[i] = stands[i].GetComponent<TurretAI>();
		}
		
		tds = new TurretData[turrets.Length];
		for (int i=0;i<turrets.Length;i++) {
			tds[i] = turrets[i].GetComponent<TurretData>();
		}

		costs = new int[totalIndex.Length-1];
		for (int i = 0;i<totalIndex.Length-1;i++) {
			if (i <= stands.Length-1) {
				costs[i] = ais[i].cost;
			}else{
				costs[i] = tds[i-ais.Length].cost;
			}
		}
	}
	
	void Update () {
		
		if (Input.GetButtonDown("Fire2")) {
			TestPosition ();
		}

		if (Input.GetButtonDown ("Fire1")) {
			focusTurret = null;
		}

		size = ais[selectedStand].size * 2;
		Vector3 newSize = new Vector3 (size,0.5f,size);
		transform.localScale = newSize;

		if (!focusTurret) {
			showTurretOptions = false;
		}

	}

	void FixedUpdate () {

		Collider[] newCols = Physics.OverlapSphere(terrain.hitPoint,size/2);
		canPlace = true;

		if (showTurretOptions == false) {
			focusTurret = null;
		}
			                               
		for (int i = 0;i<newCols.Length;i++) {
			if (newCols[i].gameObject.tag == "Freindly") {
				if (!focusTurret) {
					focusTurret = newCols[i].gameObject;
					focusAI = focusTurret.GetComponent<TurretAI>();
				}
				canPlace = false;
			}else if (newCols[i].gameObject.tag == "Enemy") {
				canPlace = false;
			}
		}
	}

	void TestPosition () {

		if (focusTurret) {
			if (focusAI.turret.transform.childCount == 0) {
				stats.credits -= tds[selectedTurret].cost;
				focusTurret.SendMessage ("GetTurretData",GetTurret ());
			}else{
				showTurretOptions = true;
			}
		}else{
			stats.credits -= ais[selectedStand].cost;
			Instantiate(GetStand (),transform.position,Quaternion.identity);
		}
	}
	
	GameObject GetTurret () {
		return turrets[selectedTurret];
	}

	GameObject GetStand () {
		return stands[selectedStand];
	}

/*	void OnDrawGizmos () {
		Gizmos.DrawWireSphere(terrain.hitPoint,size/2);
	}*/
}