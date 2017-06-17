using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUIController : MonoBehaviour {
	public Image character, dashBar;
	[HideInInspector]
	public Stamina player;

	void Update () {
		if (player != null) {
			dashBar.fillAmount = (float)(0.3f + (0.6f * (player.Staminar / player.maxStamina)));
		}
	}
}
