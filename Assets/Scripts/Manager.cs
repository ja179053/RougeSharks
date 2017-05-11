using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;

public class Manager : MonoBehaviour {
	public AudioMixer am;
	AudioSource aso;
	public Slider s;
	// Use this for initialization
	void Start () {
		am = Resources.Load ("MainAudioMixer") as AudioMixer;
		aso = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			if (Application.isEditor) {
				Debug.Break ();
			} else {
				Application.Quit ();
			}
		}
	}
	public void UpdateVolume(){
		aso.volume = s.value;
	//	am.SetFloat (GetComponent<AudioSource>().outputAudioMixerGroup.name, s.value);
	}
}
