﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;

public class MultiplayerManager : Manager
{
	public Text[] characterSetUp;

	bool[] buttons;
	//Network manager being used
	static NetworkManager nm;
	static NetworkLobbyManager nlm;
	public static int onlinePlayers = 1;
	public void Leave(){
		EndGame ();
	}
	public void Host ()
	{
		if (nlm.numPlayers == 0) {
			nlm.StartHost ();
		}
	}

	public void Join ()
	{
		nlm.TryToAddPlayer ();
	}

	new void Start ()
	{
		MusicSetUp ();
		if (nlm == null) {
			nlm = GetComponent<NetworkLobbyManager> ();
		}
	}

	public void PlayOnline(){
		if (nm == null) {
			nm = FindObjectOfType<NetworkManager> ();
		}
		if (!NetworkServer.active) {
			Debug.Log ("Server online");
			nm.StartServer ();
		}
	}

	// Detects exit input
	new void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			EndGame ();
		}
	}
	public void StartFighting ()
	{
		int i = 0, requiredPlayers = 0;
		buttons = new bool[4];
		foreach (Text t in characterSetUp) {
			if (t.text != "" + CharacterEnum.Off) {
				buttons [i] = (t.text == "" + CharacterEnum.AI);
				//If a player is in use,
				if (!buttons [i]) {
					requiredPlayers++;
				}
				//	Debug.Log (buttons [i]);
				i++;
			}
		}
		//Error checking
		if (i < 2) {
			Debug.LogError ("No players selected");
			return;
		} else if (requiredPlayers != nlm.numPlayers) {
			Debug.LogError (string.Format ("You dont have {0} players connected", requiredPlayers));
			return;
		}
		playerAIFalseCount = new bool[i];
		for (int j = 0; j < i; j++) {
			playerAIFalseCount [j] = buttons [j];
		}
		SceneManager.LoadScene (5);
	}
}
