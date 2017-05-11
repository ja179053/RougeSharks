using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Manager : MonoBehaviour {
	//The game's audio mixer
	AudioMixer am;
	//Source of theme music
	AudioSource aso;
	//Volume slider
	public Slider s;
	// Use this for audio initialisation
	void Start () {
		LevelSetUp ();
	}
	protected void LevelSetUp(){
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
	public static void EndGame(){
		Cursor.visible = true;
		SceneManager.LoadScene (1);
	}
}
