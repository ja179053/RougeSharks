using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LevelSelect : MonoBehaviour {
	public string scene;
	static NetworkLobbyManager nlm;
	public void ThisLevel(){
		if (nlm == null) {
			nlm = FindObjectOfType<NetworkLobbyManager> ();
		}
		nlm.playScene = scene;
	}
}
