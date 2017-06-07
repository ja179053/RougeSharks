using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Manager : MonoBehaviour {
	//The game's audio mixer
	AudioMixer am;
	//Source of theme music
	AudioSource aso;
	//Network manager being used
	static NetworkManager nm;
	protected static NetworkLobbyManager nlm;
	//Volume slider
	public Slider s;
	//Number of players starting the match
	protected static bool[] playerAIFalseCount;
	// Use this for audio initialisation
	void Start () {
		MusicSetUp ();
	}
	protected void MusicSetUp(){
		am = Resources.Load ("MainAudioMixer") as AudioMixer;
		aso = GetComponent<AudioSource> ();
	}
	
	// Detects exit input
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			if (Application.isEditor) {
				Debug.Break ();
			} else {
				Application.Quit ();
			}
		}
	}
	//Sets the music volume using the slider.
	public void UpdateVolume(){
		aso.volume = s.value;
	//	am.SetFloat (GetComponent<AudioSource>().outputAudioMixerGroup.name, s.value);
	}
	public void LoadOtherLevel(int i){
		SceneManager.LoadScene (i);
	}
	public static void EndGame(){
		Debug.Log ("Game over");
		Cursor.visible = true;
		SceneManager.LoadScene (1);
	}
}
