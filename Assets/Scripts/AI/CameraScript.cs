using UnityEngine;
using System.Collections;

public class CameraScript : Manager {
	Player[] players;
	public Vector3 centre;
	public float minUpdateDistance;
	// Use this for initialization
	void Start () {
		players = FindObjectsOfType<Player> ();
		centre = GameObject.Find ("Iceberg").transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Vector3.Distance (centre, FindCentre()) > minUpdateDistance) {
			centre = FindCentre ();
		}
		transform.LookAt (centre);
	//	RaycastAll ();
	}
	Vector3 FindCentre(){
		Vector3 tempCentre = Vector3.zero;
		foreach (Player p in players){
			tempCentre += p.transform.position;
		}
		tempCentre /= players.Length;
		return tempCentre;
	}
	void RaycastAll(){
		RaycastHit rh;
		foreach (Player p in players) {
			Ray r = new Ray (transform.position, p.transform.position - transform.position);
			if (Physics.Raycast (r, out rh)) {
				Debug.Log (rh.collider.name);
			}
		}
	}
}
