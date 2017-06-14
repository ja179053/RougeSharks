using UnityEngine;
using UnityEngine.Networking;

public class SelectToSet : MonoBehaviour {
	protected static NetworkLobbyManager nlm;
	protected void SetupNLM(){
		if (nlm == null) {
			nlm = FindObjectOfType<NetworkLobbyManager> ();
		}
	}
	protected void MoveToMe(Transform t, string s){
		if (t == null) {
			t = GameObject.Find (s).transform;
		}
		t.position = new Vector3 (transform.position.x, t.position.y, t.position.z);
	}
}
