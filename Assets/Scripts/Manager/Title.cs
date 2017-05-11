using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Title : Manager {

	public void StartFighting(){
		SceneManager.LoadScene (2);
	}
}
