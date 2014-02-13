using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public float cameraSens;
	public float cameraZoomSens;
	public GameObject pointer;
	public Color curserColor;
	public Color curserNegativeColor;
	Renderer pointerMat;

	public Vector3 camPos;
	public Quaternion camRot;
	public bool onTurret;
	public Transform turretView;

	MouseScript ms;

	// Use this for initialization
	void Start () {

		pointerMat = pointer.transform.FindChild ("PointerCube").gameObject.GetComponent<Renderer>();
		ms = GameObject.Find("_MOUSEPOINTER").GetComponent<MouseScript>();
	
	}
	
	void Update () {

		if (!onTurret) {
			camPos = transform.position;
			camRot = transform.rotation;
		}

		if (!ms.focusTurret) {
			pointerMat.sharedMaterial.color = curserColor;
		}else{
			pointerMat.sharedMaterial.color = curserNegativeColor;
		}

		if (ms.focusTurret) {
		}else{
		}

		if (onTurret == false) {
			transform.position += new Vector3 (0f, Input.GetAxis ("Mouse ScrollWheel") * cameraZoomSens, 0);
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
		}else{
			if (Input.GetButtonDown ("Exit")) {
				ResetView();
			}
		}
	}

	public void ChangeView (Transform newView) {
		turretView = newView;
		onTurret = true;
		transform.position = newView.position;
		transform.rotation = newView.rotation;
		transform.parent = newView;
	}

	public void ResetView () {
		onTurret = false;
		turretView = null;
		transform.position = camPos;
		transform.rotation = camRot;
		transform.parent = null;
	}
}
