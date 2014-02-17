using UnityEngine;
using System.Collections;

public class EnemyStatsController : MonoBehaviour {

	public int value;
	public float healthWaveFactor;
	public float armorWaveFactor;
	public float regenWaveFactor;
	public float maxRegenSpeed;
	public bool drawHealth;
	HealthScript health;
	StatsManager stats;
	EnemyInfantryAI ai;

	// Use this for initialization
	void Start () {

		stats = GameObject.FindGameObjectWithTag("Stats").GetComponent<StatsManager>();
		health = GetComponent<HealthScript>();
		ai = GetComponent<EnemyInfantryAI>();

		health.maxHealth += stats.wave * healthWaveFactor * (stats.difficulty * 0.2f);
		health.health += stats.wave * healthWaveFactor * (stats.difficulty * 0.2f);
		health.maxArmor += stats.wave * armorWaveFactor * (stats.difficulty * 0.2f);
		health.armor += stats.wave * armorWaveFactor * (stats.difficulty * 0.2f);
		health.regenSpeed += Mathf.Max (stats.wave * regenWaveFactor * (stats.difficulty / 0.2f),maxRegenSpeed);
		health.maxRegen = health.maxHealth/health.maxRegen * 100;

		value = stats.wave * 10 + (int)health.maxHealth/(stats.difficulty*10);

		//RandomizeSize ();
	
	}

	
	void RandomizeSize () {

		float newScale = Random.Range (1f-(float)stats.wave/100f,1f+(float)stats.wave/100f);
		newScale = Mathf.Clamp (newScale,0.5f,3f);
		transform.localScale *= newScale;
		health.maxHealth *= newScale;
		health.health *= newScale;
		ai.maxSpeed *= Mathf.Max (-(newScale) + ai.maxSpeed,0.1f);

	}

	void OnDestroy () {

		stats.credits += value;

	}

	void OnGUI () {

		if (drawHealth) {
			if (ai.model.renderer.isVisible && stats.cam.onTurret == false) {
				Vector2 camPos = Camera.main.WorldToScreenPoint(transform.position);
				Vector2 screenPos = new Vector2 (camPos.x,-(camPos.y) + Screen.height);
				GUI.Label (new Rect (screenPos.x-50,screenPos.y-45,100,20),"Armor: " + health.armor.ToString ());
				GUI.Label (new Rect (screenPos.x-50,screenPos.y-30,100,20),"Health: " + health.health.ToString ());
			}
		}

	}
}
