using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour {
	public Transform target;
	public Vector3 axis;
	public float speed = 1;
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.RotateAround (target.position, axis, speed * Time.fixedDeltaTime);
	}
}
