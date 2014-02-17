using UnityEngine;
using System.Collections;

public class ParticleScript : MonoBehaviour {

	public float life = 1;

	// Use this for initialization
	void Start () {

		Destroy (gameObject,life);
	
	}
}