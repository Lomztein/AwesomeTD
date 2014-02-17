using UnityEngine;
using System.Collections;

public class TerrainScript : MonoBehaviour {

	public GameObject pointer;
	public float terrainHeight;
	public Vector3 hitPoint = Vector3.zero;
	Ray ray;
	RaycastHit hit;
	CameraScript camScript;
	MouseScript ms;

	void Start () {

		MeshFilter mf = GetComponent<MeshFilter>();
		Mesh mesh = mf.mesh;
		Vector3[] verts = mesh.vertices;
		ms = GameObject.Find ("_MOUSEPOINTER").GetComponent<MouseScript>();

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
				hitPoint = hit.point;
	//			Debug.Log (hit.point)
				if (ms.focusTurret) {
					pointer.transform.position = ms.focusTurret.transform.position;
				}else{
					pointer.transform.position = hit.point;
				}
			}
		}
	}

	void OnDrawGizmos () {

		if (Camera.main.gameObject.activeInHierarchy == true) {
			Gizmos.DrawRay(ray.origin,ray.direction * hit.distance);
		}

	}
	
}
