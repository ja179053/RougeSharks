using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Title : Manager {
	public Text[] characterSetUp;
	public GameObject normalMenu, characterMenu;
	bool[] buttons;
	bool ready = false;
	public void StartFighting(){
		if (ready) {
			int i = 0;
			buttons = new bool[4];
			foreach (Text t in characterSetUp) {
				if (t.text != "" + CharacterEnum.Off) {
					buttons [i] = (t.text == "" + CharacterEnum.AI);
					Debug.Log (buttons [i]);
					i++;
				}
			}
			if (i < 2) {
				Debug.LogError ("No players selected");
				return;
			}
			playerAIFalseCount = new bool[i];
			for (int j = 0; j < i; j++) {
				playerAIFalseCount [j] = buttons [j];
			}
			SceneManager.LoadScene (2);
		} else {
			normalMenu.SetActive (ready);
			ready = true;
			characterMenu.SetActive (ready);
		}
	}
}
