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
	// Use this for initialization
	void Start () {
		Physics.queriesHitTriggers = true;
		t = GetComponent<Text> ();
		CharE = CharacterEnum.Off;
	}
	public void OnPointerDown(PointerEventData eventData){
		int i = (int)CharE + 1;
		if (i > 3) {
			i = 1;
		}
		CharE = (CharacterEnum)i;
	}
}
