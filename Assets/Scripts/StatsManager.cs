using UnityEngine;
using System.Collections;

public class StatsManager : MonoBehaviour {

	public bool waveStarted;
	public int difficulty;
	public int wave;
	public bool debugMode;
	public int credits;

	void Update () {

		if (Input.GetButton ("Exit")) {
			Application.Quit();
		}
		if (Input.GetButton ("Reload")) {
			Application.LoadLevel (Application.loadedLevelName);
		}
	}
}