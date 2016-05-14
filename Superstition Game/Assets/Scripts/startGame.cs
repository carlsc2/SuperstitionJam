using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour {

	public void play() {
		SceneManager.LoadScene("overworld");
	}
}
