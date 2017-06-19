using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSelect : MonoBehaviour {
	public Button[] buttons;
	int buttonNum = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		int i = (CameraScript.pausedPlayerNum == 0) ? 1 : CameraScript.pausedPlayerNum;
		float input = Input.GetAxis ("Vertical" + i);
		if (input < 0) {
			UpdatePos (-1);
		} else if (input > 0) {
			UpdatePos (1);
		}
		if (Input.GetButtonDown ("Jump" + i)) {
			buttons [buttonNum].Select ();
		}
	}
	void UpdatePos(int i){
		buttonNum = NumberTools.Loop(buttonNum + i, buttons.Length - 1, 0);
		float buttonWidth = buttons [buttonNum].GetComponent<RectTransform> ().rect.width;
		transform.parent.transform.position = new Vector3 (buttons[buttonNum].transform.position.x - buttonWidth, buttons[buttonNum].transform.position.y, 0);
		transform.localPosition = Vector2.right * (buttonWidth * 1.5f);
	}

}
