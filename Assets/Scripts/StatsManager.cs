using UnityEngine;
using System.Collections;

public class StatsManager : MonoBehaviour {

	public bool waveStarted;
	public int difficulty;
	public int wave;
	public bool debugMode;
	public int credits;
	public float timeScale = 1;
	public float fieldOfView = 90;
	public GameObject[] cores;
	public bool lostGame;
	public CameraScript cam;

	void Start () {

		cam = Camera.main.GetComponent<CameraScript>();
		//UpdateCores ();
	}

	void UpdateCores () {
		cores = GameObject.FindGameObjectsWithTag("Core");
	}

	void Update () {

		if (lostGame == true) {
			waveStarted = false;
		}

		if (Input.GetButtonDown ("Exit")) {
			if (cam.onTurret == false) {
				Application.Quit();
			}
		}
		if (Input.GetButtonDown ("Reload")) {
			Application.LoadLevel (Application.loadedLevelName);
		}

		Time.timeScale = timeScale;
		Camera.main.fieldOfView = fieldOfView;

		if (cores[0] == null) {
			lostGame = true;
		}
	}

	void OnGUI () {
		if (lostGame == true) {
			GUI.Label (new Rect(Screen.width/2,Screen.height/2,500,20),"Game over, restart game with 'Reload [DEFAULT R]'");
		}
	}
}