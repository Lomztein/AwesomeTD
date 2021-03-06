﻿using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	
	public GameObject[] enemyTypes;
	public GameObject[] bossTypes;
	public int wavesBetweenEnemies;
	private StatsManager stats;
	public float[] spawnFrequency;
	public float mapWidth;
	public int startingSpawnFrequency;
	public int minSpawnFrequency;
	
	// Use this for initialization
	void Start () {
		stats = GameObject.FindGameObjectWithTag("Stats").GetComponent<StatsManager>();
		NextWave ();
		spawnFrequency = new float[enemyTypes.Length];
		
		for (int i=0;i<spawnFrequency.Length;i++)  {
			spawnFrequency[i] = startingSpawnFrequency;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (stats.waveStarted == true) {
			int enemyNumber = -1;
			foreach (GameObject newEnemy in enemyTypes) {
				enemyNumber++;
				if (enemyNumber * wavesBetweenEnemies <= stats.wave) {
					if (Mathf.Round (Random.Range (0,spawnFrequency[enemyNumber])) == 1) {
						Instantiate(newEnemy,new Vector3(Random.onUnitSphere.x * mapWidth, transform.position.y,transform.position.z),Quaternion.identity);
					}
				}
			}
		}
	}
	
	void EndWave () {
		stats.waveStarted = false;
		Invoke ("NextWave",5);
	}
	
	void NextWave () {
		stats.waveStarted = true;
		Invoke ("EndWave",20);
		stats.wave++;
		//Debug.Log ("New wave: "+stats.wave.ToString());
		int index = -1;
		foreach (float frequency in spawnFrequency) {
			index++;
			if (index * wavesBetweenEnemies <= stats.wave && frequency > minSpawnFrequency) {
				spawnFrequency[index] -= Mathf.Max (1 * stats.difficulty,minSpawnFrequency);
			}
		}
	}
	
	void OnGUI () {
		if (stats.debugMode == true) {
			int index = 0;
			foreach (float frequency in spawnFrequency) {
				
				bool canSpawn;
				
				if (index * wavesBetweenEnemies <= stats.wave) {
					canSpawn = true;
				}else{
					canSpawn = false;
				}
				
				GUI.Label( new Rect (200, 10 + index * 20,Screen.width, 20),enemyTypes[index].name +":"+frequency.ToString()+", can spawn: "+canSpawn.ToString ());
				index++;
			}
		}
	}
}