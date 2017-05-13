using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class LobbyPlayer : MonoBehaviour {
	NetworkLobbyPlayer nlp;
	// Use this for initialization
	void Start () {
		nlp = GetComponent<NetworkLobbyPlayer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			nlp.RemovePlayer ();
			MultiplayerManager.EndGame ();
		}
	}
}
