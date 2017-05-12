using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {
	public Transform target;
	public float speed = 5;
	public Vector3 extra = Vector3.zero;
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards (transform.position, target.position + extra, Time.deltaTime * speed);
	}
}
