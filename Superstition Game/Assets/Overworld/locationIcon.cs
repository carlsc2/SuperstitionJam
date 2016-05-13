using UnityEngine;
using UnityEngine.SceneManagement;

public class locationIcon : MonoBehaviour {

	public string dest_scene;

	public void enter() {
		SceneManager.LoadScene(dest_scene);
	}
}
