using UnityEngine;
using System.Collections;

public class TerrainScript : MonoBehaviour {

	public GameObject pointer;
	Ray ray;
	RaycastHit hit;
	CameraScript camScript;
	
	// Use this for initialization
	void Start () {

		camScript = Camera.main.GetComponent<CameraScript>();

	}
	
	// Update is called once per frame
	void Update () {

		if (Camera.main.enabled == true) {
			//Find mouse position
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (collider.Raycast(ray,out hit,Mathf.Infinity)) {
	//			Debug.Log (hit.point);
				pointer.transform.position = hit.point;
			}
		}
	}

	void OnDrawGizmos () {

		if (Camera.main.gameObject.activeInHierarchy == true) {
			Gizmos.DrawRay(ray.origin,ray.direction * hit.distance);
		}

	}
}
