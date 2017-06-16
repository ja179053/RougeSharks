﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;

public class MultiplayerManager : Manager
{
	static MultiplayerManager instance;
	public Text[] characterSetUp;

	bool[] buttons;
	public static int onlinePlayers = 1;

	void Awake(){
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (this);
		} else {
			Debug.Log ("instance already exists");
			Destroy (this.gameObject);
		}
		Cursor.visible = true;
	}

	new void Start ()
	{
		MusicSetUp ();
		if (nlm == null) {
			nlm = GetComponent<NetworkLobbyManager> ();
		}
	}

	public void ListServers(){
		if (nlm.matches != null) {
	//		nlm.matchMaker.
	//		StartCoroutine(nlm.matchMaker.ListMatches());
		}
	}
	public void CreateMatch(){
		nlm.matchMaker.CreateMatch ("default", 4, false, "", "192.168.1.254", "", 0, 1, nlm.OnMatchCreate);
		SetUpPlayer (0);
	}
	public void Host ()
	{
		if (nlm.numPlayers == 0) {
		//	nlm.networkAddress = Network.player.ipAddress;
			nlm.StartHost ();
			SetUpPlayer (0);
		}
	}
	public void Join ()
	{
		nlm.TryToAddPlayer ();
		//	nlm.GetStartPosition ();
		SetUpPlayer(1);
	}
	public void SetUpPlayer(int i){
		characterSetUp [i].GetComponent<TogglePlayerSelect> ().X (1, true);
		//Show Controls for player 1
	}

	public void PlayOnline(){
		if (InitaliseGameScene ()) {
			if (!NetworkServer.active) {
				nlm.StartServer ();
			}
			nlm.ServerChangeScene (nlm.playScene);
		}
	}

	public bool InitaliseGameScene(){
		int players = 0, requiredPlayers = 0;
		buttons = new bool[4];
		//Finds out how many players there are
		foreach (Text t in characterSetUp) {
			if (t.text != "" + CharacterEnum.Off) {
				//The new button array sets each value to "true if AI"
				buttons [players] = (t.text == "" + CharacterEnum.AI);
				//If a player is in use,
				if (!buttons [players]) {
					requiredPlayers++;
				}
				//	Debug.Log (buttons [i]);
				players++;
			}
		}
		//Error checking
		if (players < 2) {
			Debug.LogError ("No players selected");
			return false;
		} else if (requiredPlayers != nlm.numPlayers) {
			Debug.LogError (string.Format ("You dont have {0} players connected. Waiting for {1} player{2}.", requiredPlayers, requiredPlayers - nlm.numPlayers,(nlm.numPlayers < 2) ? "" : "(s)"));
			return false;
		}
		//Uses number/arrangement of players to initialise game scene
		Debug.Log ("starting game");
		playerAIFalseCount = new bool[players];
		for (int j = 0; j < players; j++) {
			playerAIFalseCount [j] = buttons [j];
		}
		return true;
	}
	public void MainMenu(){
		SceneManager.LoadScene (0);		
	}
}
