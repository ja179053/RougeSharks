﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour {
	public float countSpeed = 2;
	public Text countdownText;
	public static bool started = false;
	// Use this for initialization
	void Start () {
		StartCoroutine (Play ());
	}
	int time = 4;
	IEnumerator Play(){
		time --;
		countdownText.text = "" + time;
		yield return new WaitForSeconds (countSpeed);
		if (time < 1) {
			started = true;
			countdownText.text = "";
		} else {
			StartCoroutine (Play ());
		}
	}
}