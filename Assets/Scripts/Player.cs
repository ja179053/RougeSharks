using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	//The player's rigidbody
	Rigidbody r;
	//The identity of the player and how many lives they have
	public int playerNumber = 1, lives = 1;
	//Character speed. Can be set in the inspector but is set to the fastest character's speed
	public float setSpeed = 0.1f;
	static float speed = 0;
	//Public setting for how long/far you can dash/jump for. Make static after testing.
	public float dashCooldown = 2, dashBoost = 20, jumpHeight = 2;
	//If the player is on the ground
	bool grounded;
	//Layer of ground to detect
	public LayerMask ground;
	// Initialises Rigidbody and speed. 
	void Start () {
		r = GetComponent<Rigidbody> ();
		if (setSpeed > speed) {
			speed = setSpeed;
		}
	}
	// Update is called once per frame.
	void Update () {
		if (canDash) {
			//Reads Inputs and moves the input direction.
			PowerInputs ();
			Vector3 direction = new Vector3 (Input.GetAxis ("Horizontal" + playerNumber), 0, Input.GetAxis ("Vertical" + playerNumber));
			transform.LookAt (transform.position + direction);
			r.MovePosition (transform.position + (direction * speed));
		} else {
			r.MovePosition(transform.position + (transform.forward * dashBoost * (1 + dashHoldTime)));			
		}
		//Raycast to detect when near the ground
		RaycastHit rh;
		grounded = Physics.Raycast (transform.position, Vector3.down * 0.2f, out rh);
		//Makes sure the player isn't dead
		CheckToDie();
	}
	bool canJump = true;
	//Moves the player up a little. Can't be used until the player falls back down.
	//FIX JUMP TO NOT USE ADD FORCE
	IEnumerator Jump(){
	//	Debug.Log ("jumping");
		canJump = false;
		r.AddForce(Vector3.up * jumpHeight);
	//	r.MovePosition (Vector3.up * jumpHeight);
		yield return new WaitForSeconds (0.5f);
		yield return new WaitUntil (() => grounded);
		canJump = true;
	}
	bool canDash = true;
	//Moves the player forward quickly. Limited by dash cooldown.
	IEnumerator Dash(float boost){
	//	Debug.Log ("dashing " + boost);
		canDash = false;
	//	r.MovePosition(transform.position + (transform.forward * dashBoost * (1 + boost)));
		yield return new WaitForSeconds (dashCooldown);
		canDash = true;
		dashHoldTime = 0;
	}
	float dashHoldTime = 0;
	//Reads Inputs for special powers
	void PowerInputs(){
		//Jump input to allow one when on the ground
		if (Input.GetButton ("Jump" + playerNumber)) {
			if (canJump) {
				StartCoroutine (Jump ());			
			}
		}
		//Dash input to allow every *dashCooldown* seconds
		if (Input.GetButtonUp ("Dash" + playerNumber)) {
			if (canDash) {
				StartCoroutine (Dash (dashHoldTime));
				dashHoldTime = 0;
			}
		}
		//Dash can be charged for up to 2 seconds
		if (Input.GetButton("Dash" + playerNumber)) {
			if (canDash) {
				dashHoldTime += Time.deltaTime;
				if (dashHoldTime > 2) {
					StartCoroutine (Dash (dashHoldTime));
				}
				return;
			} 
				dashHoldTime = 0;
		}
	}
	void CheckToDie(){
		if (transform.position.y < -10) {
			Die ();
		}
	}
	void Die(){
		lives--;
		if (lives == 0) {
			Manager.EndGame ();
		} else {
			GameObject respawnPoint = GameObject.Find ("Iceberg");
			transform.position = respawnPoint.transform.position + (Vector3.up * 2);
			r.velocity = Vector3.zero;
		}
	}
}
