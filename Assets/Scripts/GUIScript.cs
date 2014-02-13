using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {

	public int classIndex;
	public GameObject[] classTurrets;
	public int[] indexes;
	public StatsManager stats;
	public MouseScript ms;

	void Start () {
	
		ms = GetComponent<MouseScript>();
		stats = GameObject.FindGameObjectWithTag("Stats").GetComponent<StatsManager>();
		                             
	}

	void UpdateMenu () {

		int num = 0;

		for (int i = 0;i<ms.turrets.Length;i++) {
			if (ms.tds[i].classType == classIndex) {
				num++;
			}
		}

		classTurrets = new GameObject[num];
		indexes = new int[num];

		int index = 0;
		for (int i = 0;i<ms.turrets.Length;i++) {
			if (ms.tds[i].classType == classIndex) {
				classTurrets[index] = ms.turrets[i];
				indexes[index] = i;
				index++;
			}
		}

	}

	void OnGUI () {

		GUI.Label (new Rect(10,10,Screen.width,20),"Credits: " + stats.credits.ToString ());
		GUI.Label (new Rect(10,30,Screen.width,20),"Wave: " + stats.wave.ToString ());

		if (stats.debugMode) {

			GUI.Label (new Rect(10,50,Screen.width,20),"Difficulty: " + stats.difficulty.ToString ());

			GUI.Label (new Rect(10,80,Screen.width,20),"Selected stand: " + ms.stands[ms.selectedStand].name);
			GUI.Label (new Rect(10,100,Screen.width,20),"Selected turret: " + ms.turrets[ms.selectedTurret].name);

			if (ms.focusTurret) {
				TurretAI focusAI = ms.focusTurret.GetComponent<TurretAI>();
				GUI.Label (new Rect(10,120,Screen.width,20),"Focus turret: " + ms.focusTurret.name + " at " + ms.focusTurret.transform.position);
				GUI.Label (new Rect(10,140,Screen.width,20),"Upgrade cost: " + focusAI.upgradeCost.ToString());
				GUI.Label (new Rect(10,160,Screen.width,20),"Sell value: " + Mathf.RoundToInt((float)focusAI.upgradeCost * 0.75f));
			}

			GUI.Label (new Rect(10,190,Screen.width,20),"Stand cost: " + ms.ais[ms.selectedStand].cost.ToString());
			GUI.Label (new Rect(10,210,Screen.width,20),"Turret cost: " + ms.tds[ms.selectedTurret].cost.ToString());
			GUI.Label (new Rect(10,230,Screen.width,20),"Total cost: " + (ms.ais[ms.selectedStand].cost + ms.tds[ms.selectedTurret].cost).ToString());

			GUI.Label(new Rect(Screen.width - 200,10,190,20),"Time scale: " + stats.timeScale.ToString ());
			stats.timeScale = GUI.HorizontalSlider(new Rect(Screen.width - 200,30,190,20),stats.timeScale,0f,2f);

			GUI.Label(new Rect(Screen.width - 200,60,190,20),"Field of view: " + stats.fieldOfView.ToString ());
			stats.fieldOfView = GUI.HorizontalSlider(new Rect(Screen.width - 200,80,190,20),stats.fieldOfView,1f,179f);
		}

		if (ms.showTurretOptions == true && ms.focusTurret) {
			Vector3 screenPos = (Camera.main.WorldToScreenPoint(ms.focusTurret.transform.position));
			Vector2 pos = new Vector3 (screenPos.x,-screenPos.y + Screen.height);
			TurretAI newAI = ms.focusTurret.GetComponent<TurretAI>();
			if (GUI.Button (new Rect(pos.x-70,pos.y-20,40,40),"C")) {
				Transform newCamView = newAI.cameraView;
				Camera.main.GetComponent<CameraScript>().ChangeView (newCamView);
				ms.focusTurret = null;
				ms.showTurretOptions = false;
			}

			if (stats.credits >= newAI.upgradeCost) {
				if (GUI.Button (new Rect(pos.x-20,pos.y-70,40,40),"U")) {
					stats.credits -= newAI.upgradeCost;
					newAI.Upgrade ();
					ms.focusTurret = null;
					ms.showTurretOptions = false;
				}
			}

			if (GUI.Button (new Rect(pos.x-20,pos.y+30,40,40),"S")) {
				Destroy(newAI.gameObject);
				stats.credits += Mathf.RoundToInt ((float)newAI.upgradeCost * 0.75f);
				ms.focusTurret = null;
				ms.showTurretOptions = false;
			}
		}

		for (int i=0;i<ms.stands.Length;i++) {

			if (ms.ais[i].cost <= stats.credits) {
				if (GUI.Button(new Rect(10+i * 60,Screen.height - 120,50,50),(i+1).ToString())) {
					ms.selectedStand = i;
					classIndex = i+1;
					UpdateMenu ();
				}
			}
		}

		for (int i=0;i<classTurrets.Length;i++) {
			if (ms.tds[i].cost <= stats.credits) {
				if (GUI.Button(new Rect((10+i*60),Screen.height - 60,50,50),classTurrets[i].name)) {
					ms.selectedTurret = indexes[i];
				}
			}
		}
	}
}