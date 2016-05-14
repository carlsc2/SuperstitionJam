using UnityEngine;
using System.Collections;

public class obelisk : MonoBehaviour, Interactable {

	public virtual void Interact(Transform t) {
		GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerPawn>().KillPawn();
	}
}
