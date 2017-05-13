using UnityEngine;
using System.Collections;
using PlayerControlled;

public class CameraScript : Manager
{
	Player[] players;
	public Transform[] startPoints;
	public Vector3 centre;
	public float minUpdateDistance;
	public GameObject staminaBar, newPlayer, newEnemy;
	// Use this for initialization
	void Start ()
	{
		SetupPlayers ();
		MusicSetUp ();
		Cursor.visible = false;
	}
	public GameObject pauseMenu, optionsMenu;
	bool paused;
	public bool Paused{
		get{
			return paused;
		} set {
			paused = value;
			Time.timeScale = (paused) ? 0 : 1;
			pauseMenu.SetActive (paused);
			ToggleOptionsDispay (true);
			Cursor.visible = paused;
		}
	}
	public void ToggleOptionsDispay(bool mustDisable){
		if (mustDisable) {
			optionsMenu.SetActive (false);
			pauseMenu.SetActive (Paused);
			return;
		}
		optionsMenu.SetActive (!optionsMenu.activeSelf);
		pauseMenu.SetActive (!optionsMenu.activeSelf);
	}


	void SetupPlayers ()
	{
		try {
			players = new Player[playerAIFalseCount.Length];
			Debug.Log(players.Length);
			for (int i = 0; i < playerAIFalseCount.Length; i++) {
				Player p;
				if (playerAIFalseCount [i] == false) {
		//			p = GameObject.Instantiate (newPlayer, startPoints [i].position, Quaternion.identity) as Player;
				} else {
					GameObject g = GameObject.Instantiate (newEnemy, startPoints [i].position, Quaternion.identity) as GameObject;
					p = g.GetComponent<Player>();
					Debug.Log("instantiated" + i);
					p.stamina = GameObject.Instantiate (staminaBar).GetComponent<Stamina> ();
					p.stamina.GetComponent<Follow> ().target = p.transform;
					p.playerNumber = i + 1;
					players [i] = p;
				}
			}
		} catch {
			players = FindObjectsOfType<Player> ();
			for(int i = 0; i < players.Length; i++){
				players [i].playerNumber = i + 1;
				players[i].stamina = GameObject.Instantiate (staminaBar).GetComponent<Stamina> ();
				players[i].stamina.GetComponent<Follow> ().target = players[i].transform;
			}
		}
		Debug.Log ("level setup complete");
		centre = Player.respawnPoint;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (players.Length == 1) {
			centre = players [0].transform.position;
		} else if (players.Length > 1) {
			if (Vector3.Distance (centre, FindCentre ()) > minUpdateDistance) {
				centre = FindCentre ();
			}
			RaycastAll ();
		}
		transform.LookAt (centre);
	}

	new void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			//EndGame ();
			Paused = !Paused;
		}
	}

	Vector3 FindCentre ()
	{
		Vector3 tempCentre = Vector3.zero;
		foreach (Player p in players) {
			tempCentre += p.transform.position;
		}
		tempCentre /= players.Length;
		return tempCentre;
	}

	public float zoomSpeed = 5;

	void RaycastAll ()
	{
		/*RaycastHit rh;
		foreach (Player p in players) {
			Ray r = new Ray (transform.position, p.transform.position - transform.position);
			if (Physics.Raycast (r, out rh)) {
				Debug.Log (rh.collider.name);
			}
		}*/
		//Negative numbers make the camera zoom in. Positive numbers make the camera zoom out.
		float zoom = -zoomSpeed;
		foreach (Player p in players) {
			if (!p.smr.IsVisibleFromMain()) {
			//	Debug.Log ("zoom out soft");
				zoom *= -1;
				break;
			} 
		}
		Zoom(zoom);

	}
	void Zoom(float f){
		//	Debug.Log (zoom);
		f += Camera.main.fieldOfView;
		Camera.main.fieldOfView = Mathf.Clamp (f, 30, 120);
	}
}
