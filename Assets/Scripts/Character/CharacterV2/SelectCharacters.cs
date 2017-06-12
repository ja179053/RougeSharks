using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacters : MonoBehaviour {
	static Transform icon;
	public static SharkEnum currentShark = SharkEnum.Blue;
	public int pos = 0;
	// Update is called once per frame
	void OnMouseDown () {
		if (icon == null) {
			icon = GameObject.Find ("CurrentPlayer").transform;
		}
		icon.position = new Vector3 (transform.position.x, icon.position.y, icon.position.z);
		currentShark = (SharkEnum)pos;
	}
}
