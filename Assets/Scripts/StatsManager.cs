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

	void Update () {

		if (Input.GetButton ("Exit")) {
			Application.Quit();
		}
		if (Input.GetButton ("Reload")) {
			Application.LoadLevel (Application.loadedLevelName);
		}

		Time.timeScale = timeScale;
		Camera.main.fieldOfView = fieldOfView;
	}
}