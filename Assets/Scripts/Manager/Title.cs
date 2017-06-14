using UnityEngine;
using System.Collections;

public class Title : Manager {

	// Detects exit input
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			if (Application.isEditor) {
				Debug.Break ();
			} else {
				Application.Quit ();
			}
		}
	}
}
