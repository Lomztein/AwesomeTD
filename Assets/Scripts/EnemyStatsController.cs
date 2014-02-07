using UnityEngine;
using System.Collections;

public class EnemyStatsController : MonoBehaviour {

	public int value;
	public float healthWaveFactor;
	public float armorWaveFactor;
	public float regenWaveFactor;
	public float maxRegenSpeed;
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

		value = (((int)health.maxHealth + (int)health.maxArmor) * Mathf.Max ((int)health.regenSpeed,1))/5;

		RandomizeSize ();
	
	}
	
	void RandomizeSize () {

		float newScale = Random.Range (1f-(float)stats.wave/100f,1f+(float)stats.wave/100f);
		Mathf.Clamp (newScale,0.3f,3f);
		transform.localScale *= newScale;
		health.maxHealth *= newScale;
		health.health *= newScale;
		ai.maxSpeed *= Mathf.Max (-(newScale) + ai.maxSpeed,0.1f);

	}

	void OnDestroy () {

		stats.credits += value;

	}
}
