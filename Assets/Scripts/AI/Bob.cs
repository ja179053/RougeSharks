using UnityEngine;
using System.Collections;

public class Bob : MonoBehaviour {
	public Vector3 direction;
	Vector3 initialPosition;
	public float speed = 1, distance = 1;
	
	// Update is called once per frame
	void Update () {
		if (initialPosition == null) {
			initialPosition = transform.position;
		}
		float x = Mathf.Sin (direction.x * Time.time) * distance;
		float y = Mathf.Sin (direction.y * Time.time) * distance;
		float z = Mathf.Sin (direction.z * Time.time) * distance;
	//	Debug.Log (y);
		transform.position = (speed * new Vector3(x,y,z)) + initialPosition;
	}
}
