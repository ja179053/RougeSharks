using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	Rigidbody r;
	public float playerNumber;
	public float setSpeed = 0.1f;
	static float speed = 0;
	static float dashCooldown = 2;
	// Use this for initialization
	void Start () {
		r = GetComponent<Rigidbody> ();
		if (setSpeed > speed) {
			speed = setSpeed;
		}
	}
	bool grounded;
	public LayerMask ground;
	// Update is called once per frame
	void Update () {
		Vector3 direction = new Vector3 (Input.GetAxis ("Horizontal" + playerNumber), 0, Input.GetAxis ("Vertical" + playerNumber));
		transform.LookAt (transform.position + direction);
		r.MovePosition(transform.position + (direction * speed));
		if (Input.GetButton ("Dash" + playerNumber)) {
			if (canDash) {
				StartCoroutine (Dash ());
			}
		}
		if (Input.GetButton ("Jump" + playerNumber)) {
			if (canJump) {
				StartCoroutine (Jump ());			
			}
		}
		RaycastHit rh;
		grounded = Physics.Raycast (transform.position, Vector3.down, out rh);
	}
	bool canJump = true;
	IEnumerator Jump(){
		r.MovePosition (Vector3.up);
		canJump = false;
		yield return new WaitUntil (() => grounded);
		canJump = true;
	}
	bool canDash = true;
	IEnumerator Dash(){
		canDash = false;
		r.MovePosition(transform.position + (transform.forward * 2));
		yield return new WaitForSeconds (dashCooldown);
		canDash = true;
	}
}
