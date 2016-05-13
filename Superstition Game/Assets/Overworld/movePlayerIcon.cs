using UnityEngine;
using System.Collections;

public class movePlayerIcon : MonoBehaviour {

	public float speed = 10.0F;

	void Update() {
		transform.Translate(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
	}
}
