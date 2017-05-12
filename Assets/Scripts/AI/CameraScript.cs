using UnityEngine;
using System.Collections;

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
		LevelSetUp ();
		//Disabled until volume in in pause menu (not created)
		//	Cursor.visible = false;
	}

	void SetupPlayers ()
	{
		try {
			players = new Player[playerAIFalseCount.Length];
			Debug.Log(players.Length);
			for (int i = 0; i < playerAIFalseCount.Length; i++) {
				Player p;
				if (playerAIFalseCount [i] == false) {
					p = GameObject.Instantiate (newPlayer, startPoints [i].position, Quaternion.identity) as Player;
				} else {
					p = GameObject.Instantiate (newEnemy, startPoints [i].position, Quaternion.identity) as Player;
				}
				Debug.Log("instantiated" + i);
				p.stamina = GameObject.Instantiate (staminaBar).GetComponent<Stamina> ();
				p.stamina.GetComponent<Follow> ().target = p.transform;
				p.playerNumber = i + 1;
				players [i] = p;
			}
		} catch {
			players = FindObjectsOfType<Player> ();
			foreach (Player p in players) {
				p.stamina = GameObject.Instantiate (staminaBar).GetComponent<Stamina> ();
				p.stamina.GetComponent<Follow> ().target = p.transform;
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
		if (Input.GetKey (KeyCode.Escape)) {
			EndGame ();
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
		float zoom = -zoomSpeed;
		foreach (Player p in players) {
			if (!p.GetComponent<Renderer> ().isVisible) {
				Debug.Log ("zoom out");
				zoom *= 1;
			} 
		}
		zoom -= Camera.main.fieldOfView;
		Camera.main.fieldOfView = Mathf.Clamp (zoom, 60, 120);
	}
}
