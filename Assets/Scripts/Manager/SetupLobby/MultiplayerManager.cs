using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;

public class MultiplayerManager : Manager
{
	public Text[] characterSetUp;

	bool[] buttons;
	public static int onlinePlayers = 1;

	void Awake(){
		DontDestroyOnLoad (this);
	}
	public void Leave(){
		nlm.StopHost ();
		Debug.Log (nlm.matchHost);
		EndGame ();
	}

	public void ListServers(){
		Debug.Log(nlm.matchInfo);
	}
	public void Connect(){
		Network.Connect ("127.0.0.1", 25000);
	}
	public void Host ()
	{
		if (nlm.numPlayers == 0) {
		//	nlm.networkAddress = Network.player.ipAddress;
			nlm.StartHost ();
			characterSetUp [0].GetComponent<TogglePlayerSelect> ().X (1);
		}
	}

	public void Join ()
	{
		nlm.TryToAddPlayer ();
	//	nlm.GetStartPosition ();
		characterSetUp [nlm.numPlayers - 1].GetComponent<TogglePlayerSelect> ().X (1);
	}

	new void Start ()
	{
		MusicSetUp ();
		if (nlm == null) {
			nlm = GetComponent<NetworkLobbyManager> ();
		}
	}

	public void PlayOnline(){
		if (InitaliseGameScene ()) {
			if (nm == null) {
				nm = FindObjectOfType<NetworkManager> ();
			}
			if (!NetworkServer.active) {
				Debug.Log ("Server online");
				nm.StartServer ();
			}
			nm.ServerChangeScene (nlm.playScene);
		}
	}

	public bool InitaliseGameScene(){
		int players = 0, requiredPlayers = 0;
		buttons = new bool[4];
		//Finds out how many players there are
		foreach (Text t in characterSetUp) {
			if (t.text != "" + CharacterEnum.Off) {
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
}
