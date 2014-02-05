using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {

	MouseScript ms;
	MeshRenderer mr;
	MeshFilter mf;

	void Start () {

	
		mr = GetComponent<MeshRenderer>();
		ms = GetComponent<MouseScript>();
		mf = GetComponent<MeshFilter>();

	}
	void OnGUI () {

		if (GUI.Button(new Rect(10,Screen.height-30,20,20),"1")) {
			ms.selectedIndex = 0;
		}
		if (GUI.Button(new Rect(40,Screen.height-30,20,20),"2")) {
			ms.selectedIndex = 1;
		}
		if (GUI.Button(new Rect(70,Screen.height-30,20,20),"3")) {
			ms.selectedIndex = 2;
		}
		if (GUI.Button(new Rect(100,Screen.height-30,20,20),"4")) {
			ms.selectedIndex = 3;
		}
		if (GUI.Button(new Rect(130,Screen.height-30,20,20),"5")) {
			ms.selectedIndex = 4;
		}

	}
}