using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using PlayerControlled;

public class CameraScript : Manager
{
	public static Vector3 centre;
	public float minUpdateDistance;
	// Use this for initialization
	void Start ()
	{
		MusicSetUp ();
		Cursor.visible = false;
		startRot = transform.rotation;
		deadPlayers = new List<int> ();
	}
	public GameObject pauseMenu, optionsMenu;
	Quaternion startRot;
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

	public static Player[] players;
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (players != null) {
			if (players.Length == 1) {
				centre = players [0].transform.position;
				transform.LookAt (centre);
			} else if (players.Length > 1) {
				transform.rotation = startRot;
		/*		if (Vector3.Distance (centre, FindCentre ()) > minUpdateDistance) {
					centre = FindCentre ();
				}
				RaycastAll ();*/
			}
	//		transform.LookAt (centre);
		}
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
			Debug.Log (p.name);
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
			if (!p.dead && !p.smr.IsVisibleFromMain()) {
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
		Camera.main.fieldOfView = Mathf.Clamp (f, 35, 120);
	}
	static List<int> deadPlayers;
	public static IEnumerator Die(int playerNum){
		deadPlayers.Add (playerNum);
		if ((players.Length - deadPlayers.Count) < 2) {
			Debug.Log ("Game over");
			for (int i = 1; i < 5; i++){
				if (!deadPlayers.Contains(i)){
					players [i - 1].dead = true;
					break;
				}
			}
			yield return new WaitForSeconds (1);
			EndGame ();
		} 
		yield return new WaitForSeconds(0);
	}
}
