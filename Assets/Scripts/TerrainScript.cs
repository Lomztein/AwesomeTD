using UnityEngine;
using System.Collections;

public class TerrainScript : MonoBehaviour {

	public GameObject pointer;
	public float terrainHeight;
	Ray ray;
	RaycastHit hit;
	CameraScript camScript;

	void Start () {

		MeshFilter mf = GetComponent<MeshFilter>();
		Mesh mesh = mf.mesh;
		Vector3[] verts = mesh.vertices;

		for (int i=0;i<verts.Length;i++) {
			verts[i] = new Vector3 (verts[i].x,verts[i].y + Random.Range(0f,terrainHeight),verts[i].z);
		}

		mesh.vertices = verts;
		mf.mesh = mesh;
		GetComponent<MeshCollider>().sharedMesh = mesh;

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
