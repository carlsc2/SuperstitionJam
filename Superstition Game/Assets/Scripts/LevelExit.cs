using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour, Interactable {

	public void Interact(Transform t) {
		SceneManager.LoadScene("overworld");
	}
}
