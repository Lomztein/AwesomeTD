using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public float cameraSens;
	public float cameraZoomSens;
	public GameObject pointer;
	public Color curserColor;
	Renderer pointerMat;

	// Use this for initialization
	void Start () {

		pointerMat = pointer.transform.FindChild ("PointerCube").gameObject.GetComponent<Renderer>();
		pointerMat.sharedMaterial.color = curserColor;
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.position += new Vector3 (0f, Input.GetAxis ("Mouse ScrollWheel") * cameraZoomSens, 0);
		/*float newRot = transform.position.y * 5;
		Mathf.Clamp (newRot,0,90);
		Debug.Log (newRot);
		transform.rotation.eulerAngles = new Vector3 (newRot,0,0);*/

		//Control camera
		Vector3 mp = Input.mousePosition;
		if (mp.x < 10) {
			transform.position += new Vector3(-cameraSens * Time.deltaTime,0f,0f);
		}
		if (mp.x > Screen.width - 10) {
			transform.position += new Vector3(cameraSens * Time.deltaTime,0f,0f);
		}
		if (mp.y < 10) {
			transform.position += new Vector3(0f,0f,-cameraSens * Time.deltaTime);
		}
		if (mp.y > Screen.height - 10) {
			transform.position += new Vector3(0f,0f,cameraSens * Time.deltaTime);
		}

	}
}
