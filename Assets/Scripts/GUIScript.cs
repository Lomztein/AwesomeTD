using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {

	StatsManager stats;
	MouseScript ms;
//	MeshRenderer mr;
//	MeshFilter mf;
	TurretAI[] ais;
	TurretData[] tds;

	void Start () {
	
//		mr = GetComponent<MeshRenderer>();
		ms = GetComponent<MouseScript>();
//		mf = GetComponent<MeshFilter>();
		stats = GameObject.FindGameObjectWithTag("Stats").GetComponent<StatsManager>();

		ais = new TurretAI[ms.stands.Length];
		for (int i=0;i<ms.stands.Length;i++) {
			ais[i] = ms.stands[i].GetComponent<TurretAI>();
		}

		tds = new TurretData[ms.turrets.Length];
		for (int i=0;i<ms.turrets.Length;i++) {
			tds[i] = ms.turrets[i].GetComponent<TurretData>();
		}
		                                  

	}
	void OnGUI () {

		GUI.Label (new Rect(10,10,Screen.width,20),"Credits: " + stats.credits.ToString ());

		GUI.Label (new Rect(10,30,Screen.width,20),"Wave: " + stats.wave.ToString ());
		GUI.Label (new Rect(10,50,Screen.width,20),"Difficulty: " + stats.difficulty.ToString ());

		if (ms.totalIndex[ms.selectedIndex]) {
			
			GUI.Label (new Rect(10,80,Screen.width,20),"Unit: " + ms.totalIndex[ms.selectedIndex].name);
			
			if (ms.selectedIndex <= ms.stands.Length) {
				GUI.Label (new Rect(10,100,Screen.width,20),"Cost: " + ais[ms.selectedIndex-1].cost);
			}else{
				GUI.Label (new Rect(10,100,Screen.width,20),"Cost: " + tds[ms.selectedIndex-ms.stands.Length-1].cost);
			}
		}

		for (int i=0;i<ms.totalIndex.Length-1;i++) {

			if (i+1 <= ms.stands.Length) {
				if (ais[i].cost <= stats.credits) {
					if (GUI.Button(new Rect(10+i * 60,Screen.height - 120,50,50),(i+1).ToString())) {
						ms.selectedIndex = i+1;
					}
				}
			}else{
				if (tds[i-ms.stands.Length].cost <= stats.credits) {
					if (GUI.Button(new Rect((10+i*60)-(ms.stands.Length*60),Screen.height - 60,50,50),ms.totalIndex[i+1].name)) {
						ms.selectedIndex = i+1;
					}
				}
			}
		}
	}
}