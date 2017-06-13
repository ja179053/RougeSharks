using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class TogglePlayerSelect : MonoBehaviour, IPointerDownHandler {
	CharacterEnum charE;
	CharacterEnum CharE{
		get{
			return charE;
		} set {
			t.text = "" + value;
			charE = value;
		}
	}
	Text t;
	public int playerNumber;
	// Use this for initialization
	void Start () {
		Physics.queriesHitTriggers = true;
		t = GetComponent<Text> ();
		CharE = CharacterEnum.Off;
	}
	public void OnPointerDown(PointerEventData eventData){
		int i = NumberTools.Loop ((int)CharE + 1, 3, 1);
		X (i);
	}
	public void X(int i){
		if (i == (int)CharacterEnum.Player) {
			if (playerNumber > (MultiplayerManager.onlinePlayers * 2)) {
				i = NumberTools.Loop (i + 1, 3, 1);
			}
		}
		CharE = (CharacterEnum)i;
	}
}
