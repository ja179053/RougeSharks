using System;
using UnityEngine;
using UnityEngine.Networking;
using PlayerControlled;

public class EnemySpawner : NetworkBehaviour
{
	Player[] players;
	public GameObject staminaBar;
	public PlayerUIController[] playerUI;
	public Transform[] startPoints;

	public void Start ()
	{
		Player[] tempPlayers = FindObjectsOfType<Player> ();
		players = new Player[Manager.playerAIFalseCount.Length];
		for (int i = 0; i < players.Length; i++) {
			if (i < tempPlayers.Length) {
				players [i] = tempPlayers [i];
			} else {
				GameObject g;
				if (Manager.playerAIFalseCount [i] == true) {
					if (Manager.nlm.spawnPrefabs [0] == null) {
						continue;
					}
					Debug.Log (Manager.nlm);
					g = GameObject.Instantiate (Manager.nlm.spawnPrefabs [0], startPoints [i].position, Quaternion.identity) as GameObject;
				} else {
					g = GameObject.Instantiate (Manager.nlm.gamePlayerPrefab, startPoints [i].position, Quaternion.identity) as GameObject;
				}
		//		NetworkServer.Spawn (g);
				players [i] = g.GetComponent<Player> ();
			}
			players [i].stamina = GameObject.Instantiate (staminaBar).GetComponent<Stamina> ();
			players [i].stamina.GetComponent<Follow> ().target = players [i].transform;
			playerUI [i].player = players [i].stamina;
			players [i].playerNumber = i + 1;
		}
		for (int i = players.Length; i < 4; i++) {
			playerUI [i].gameObject.SetActive (false);
		}
		Debug.Log ("level setup complete");
		CameraScript.centre = Player.respawnPoint;
		CameraScript.players = players;
	}
}

