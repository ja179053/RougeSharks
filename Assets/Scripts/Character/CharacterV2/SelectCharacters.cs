using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SelectCharacters : MonoBehaviour {
	static Transform icon;
	static NetworkLobbyManager nlm;
//	public static SharkEnum currentShark = SharkEnum.Blue;
	public GameObject sharkPrefab;
	public int pos = 0;
	// Update is called once per frame
	void OnMouseDown () {
		if (icon == null) {
			icon = GameObject.Find ("CurrentPlayer").transform;
		}
		if (nlm == null) {
			nlm = FindObjectOfType<NetworkLobbyManager> ();
		}
		icon.position = new Vector3 (transform.position.x, icon.position.y, icon.position.z);
//		currentShark = (SharkEnum)pos;
		nlm.playerPrefab = sharkPrefab;
	}
}
