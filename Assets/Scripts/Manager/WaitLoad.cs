using UnityEngine;
using System.Collections;

public class WaitLoad : Manager {
	public float waitTime = 5, lightIntensity = 0.75f;
	public Light mainLight;
	float targetIntensity;
	// Use this for initialization
	void Start () {
		targetIntensity = lightIntensity;
		if (mainLight == null) {
			mainLight = FindObjectOfType<Light> ();
		}
		mainLight.intensity = 0;
		StartCoroutine(LoadNextLevel());
	}
	IEnumerator LoadNextLevel(){
		yield return new WaitForSeconds (waitTime);
		EndGame ();
	}

	// Update is called once per frame
	void Update () {
		if (mainLight.intensity > lightIntensity *0.9f) {
			targetIntensity = 0;
		}
		mainLight.intensity = Mathf.Lerp (mainLight.intensity, targetIntensity, Time.deltaTime);
	}
}
