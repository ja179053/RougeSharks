using UnityEngine;

public class SelectCharacters : SelectToSet {
	static Transform icon;
//	public static SharkEnum currentShark = SharkEnum.Blue;
	public GameObject sharkPrefab;
	public int pos = 0;
	// Update is called once per frame
	void OnMouseDown () {
		SetupNLM ();
		MoveToMe (icon, "CurrentPlayer");
//		currentShark = (SharkEnum)pos;
		nlm.gamePlayerPrefab = sharkPrefab;
	}
}
