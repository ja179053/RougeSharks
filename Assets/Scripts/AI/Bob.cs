using UnityEngine;
using System.Collections;

public class Bob : MonoBehaviour
{
	public Vector3 direction;
	public Vector3 initialPosition;
	public float speed = 1, distance = 1;
	float randomFloat;

	void Start ()
	{
		randomFloat = Random.Range (-distance, distance);
		initialPosition = transform.position;
	}
	// Update is called once per frame
	void Update ()
	{
		float x = Mathf.Sin (direction.x * (Time.time + randomFloat)) * distance;
		float y = Mathf.Sin (direction.y * (Time.time + randomFloat)) * distance;
		float z = Mathf.Sin (direction.z * (Time.time + randomFloat)) * distance;
		transform.position = (speed * new Vector3 (x, y, z)) + initialPosition;
	}
}
