using UnityEngine;
using System.Collections;

public class movePlayerIcon : MonoBehaviour {

	public float speed = 1.0F;

	private RectTransform moveArea;
	private RectTransform playerIcon;

	private locationIcon curloc; //current location

	private static Vector2 current_position;

	void Awake() {
		playerIcon = transform as RectTransform;
		playerIcon.position = current_position;
		RectTransform canvas = playerIcon;
		while (canvas.parent != null && canvas.parent is RectTransform) {
			canvas = canvas.parent as RectTransform;
		}
		moveArea = canvas;
	}

	void Update() {
		//move up/down/left/right but constrain to map
		Vector3 moveVec = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		TimeManager.pass_time((int)moveVec.sqrMagnitude);//elapse time based on distance moved
		transform.Translate(moveVec * speed);
		ClampToMap();
		current_position = transform.position;

		if (curloc != null &&  Input.GetButton("Submit")) {
			//enter current location
			curloc.enter();
		}

	}

	
	private void ClampToMap() {
		// Clamp icon to map
		Vector3 pos = playerIcon.localPosition;

		Vector3 minPosition = moveArea.rect.min - playerIcon.rect.min;
		Vector3 maxPosition = moveArea.rect.max - playerIcon.rect.max;

		pos.x = Mathf.Clamp(playerIcon.localPosition.x, minPosition.x, maxPosition.x);
		pos.y = Mathf.Clamp(playerIcon.localPosition.y, minPosition.y, maxPosition.y);

		playerIcon.localPosition = pos;
	}

	void OnTriggerEnter2D(Collider2D col) {
		locationIcon tmp = col.transform.root.GetComponentInChildren<locationIcon>();
		if (tmp != null) {
			curloc = tmp;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		curloc = null;
	}
}
