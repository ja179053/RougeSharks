using UnityEngine;
using System.Collections;
using PlayerControlled;

public class Enemy : Player {
	public Transform target;
	public float maxDistance = 20, sightDistance = 5;
	new void Start(){
		CharacterSetUp ();
	//	speed /= 2;
	}
	// Update is called once per frame
	new void Update () {
		if (Countdown.started) {
			Look ();
			Move ();
		}
	}
	void Look(){
		//Dashes the target when close enough to the target. If no target is assigned, looks for a player
		RaycastHit rh;
		if (Physics.Raycast (transform.position, transform.forward * sightDistance, out rh)) {
			if (target == null) {
				if (rh.collider.tag == "Player") {
					target = rh.collider.transform;
				}
			} else {
				Debug.Log ("Enemy charging dash");
				ChargeDash ();
			}
		}
		if (target != null) {
			if (Physics.Raycast (transform.position, transform.forward * sightDistance / 2, out rh)) {
				if (rh.collider.tag == "Player") {
					if (canDash) {
						Debug.Log ("Enemy dashing");
						StartCoroutine (Dash ());
						return;
					}
				}
			}
		}
	}
	void Move(){
		Vector3 direction = Vector3.zero;
		if (Vector3.Distance (transform.position, respawnPoint) > maxDistance || target == null) {
			//Returns to the centre when too far. 
			direction = respawnPoint - transform.position;
			if (canJump) {
				StartCoroutine (Jump ());
			}
		} else{
			if (target != null) {
				//Chases the target
				direction = target.position - transform.position;
			}
		}
		Inputs (direction);
	}
}
