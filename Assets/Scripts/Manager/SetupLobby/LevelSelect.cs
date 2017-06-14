using UnityEngine;

public class LevelSelect : SelectToSet {
	public string scene;
	static Transform selectedLevel;
	public void ThisLevel(){
		SetupNLM ();
		nlm.playScene = scene;
		MoveToMe (selectedLevel, "SelectedLevel");
	}
}
