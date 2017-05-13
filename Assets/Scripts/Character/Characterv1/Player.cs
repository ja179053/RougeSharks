using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	//The player's rigidbody
	Rigidbody r;
	//Respawn point (single for now)
	public static Vector3 respawnPoint;
	//The identity of the player and how many lives they have
	public int playerNumber = 1, lives = 1;
	//Character speed. Can be set in the inspector but is set to the fastest character's speed
	public float setSpeed = 0.5f;
	protected static float speed = 0;
	//Public setting for how long/far you can dash/jump for. Make static after testing.
	public float dashCooldown = 1, dashBoost = 2, jumpHeight = 1000;
	public Stamina stamina;
//	float staminaDamage = 0.5f;	
	//If the player is on the ground
	bool grounded;
	//Layer of ground to detect
	public LayerMask ground;
	public SkinnedMeshRenderer smr;
	Animator anim;
	void Start () {
		CharacterSetUp ();
	}
	// Initialises Rigidbody and speed. 
	protected void CharacterSetUp(){
		r = GetComponent<Rigidbody> ();
		smr = GetComponentInChildren<SkinnedMeshRenderer> ();
		anim = GetComponentInChildren<Animator> ();
		respawnPoint = GameObject.Find ("Iceberg").transform.position + (Vector3.up * 2);
		if (setSpeed > speed) {
			speed = setSpeed;
		}
	}
	// Update is called once per frame.
	void Update () {
		Inputs (new Vector3(Input.GetAxis ("Horizontal" + playerNumber), 0 ,Input.GetAxis ("Vertical" + playerNumber)));
	}
	protected void Inputs(Vector3 input){
		if (canDash) {
			//Reads Inputs and moves the input direction.
			float x,z;
			x = Mathf.Clamp (input.x, -1, 1);
			z = Mathf.Clamp (input.z, -1, 1);
			Vector3 direction = new Vector3 (x, 0, z);
			anim.SetBool ("Idle", Idle(direction));
			anim.SetBool ("Fast", Fast(direction));
			transform.LookAt (transform.position + direction);
			r.MovePosition (transform.position + (direction * speed));
			PowerInputs ();
		} else {
			r.MovePosition(transform.position + (transform.forward * dashBoost * (1 + dashHoldTime)));			
		}
		RunPlayer ();
	}
	void RunPlayer(){
		stamina.Drain (-Time.deltaTime * stamina.staminaRegain);
		//Raycast to detect when near the ground
		RaycastHit rh;
		grounded = Physics.Raycast (transform.position, Vector3.down * 0.2f, out rh);
		//Makes sure the player isn't dead
		CheckToDie();
	}
	protected bool canJump = true;
	//Moves the player up a little. Can't be used until the player falls back down.
	//Add force looks nicer
	protected IEnumerator Jump(){
	//	Debug.Log ("jumping");
		canJump = false;
		anim.SetBool ("Jumping", canJump);
		r.AddForce(Vector3.up * jumpHeight);
	//	r.MovePosition (transform.position + (Vector3.up * jumpHeight));
		yield return new WaitForSeconds (0.5f);
		yield return new WaitUntil (() => grounded);
		canJump = true;
		anim.SetBool ("Jumping", canJump);
	}
	protected bool canDash = true;
	//Moves the player forward quickly. Limited by dash cooldown.
	protected IEnumerator Dash(){
	//	Debug.Log ("dashing " + boost);
		canDash = false;	
		stamina.Drain(dashHoldTime);
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
				StartCoroutine (Dash ());
				return;
			}
		}
		//Dash can be charged for up to 2 seconds
		if (Input.GetButton("Dash" + playerNumber)) {
			if (canDash) {
				ChargeDash ();
				return;
			} 
				dashHoldTime = 0;
		}
	}
	protected void ChargeDash(){
		dashHoldTime += Time.deltaTime;
		if (dashHoldTime > dashBoost || dashHoldTime > stamina.Staminar) {
			StartCoroutine (Dash ());
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
			transform.position = respawnPoint;
			r.velocity = Vector3.zero;
			stamina.Drain (-stamina.maxStamina);
		}
	}
	public void Damage(float f){
		stamina.Drain(f);
	}
	static bool Idle(Vector3 v){
		if (v.x < 0.1f && v.z < 0.1f && v.x > -0.1f && v.z > -0.1f) {
			return true;
		}
		return false;
	}
	static bool Fast(Vector3 v){
		//0.5 when walk is done
		if (v.x < -0.5f || v.z < -0.5f || v.x > 0.5f || v.z > 0.5f) {
			return true;
		}
		return false;
	}
}
