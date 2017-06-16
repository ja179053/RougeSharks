using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleOnline : MonoBehaviour {
	bool online = false;
	public bool Online{
		get {return online; } set {
			GetComponentInChildren<Text>().text = (value == false) ? "Online Mode" : "VS Mode";
			onlineButtons.SetActive (value);
			vsButtons.SetActive (!value);
			online = value; 
			if (value == true) {
				MultiplayerManager.nlm.StartMatchMaker ();
			} else {
				MultiplayerManager.nlm.StopMatchMaker ();
			}
		}
	}
	public GameObject onlineButtons, vsButtons;
	// Update is called once per frame
	public void Toggle () {
		Online = !Online;
	}
}
