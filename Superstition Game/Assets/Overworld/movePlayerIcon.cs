using UnityEngine;
using System.Collections;

public class movePlayerIcon : MonoBehaviour {

	public float speed = 1.0F;

	public static float ticks_per_day = 400; // number of move ticks per day

	public static float timeofday = 100; //make time go between 0 and ticks_per_day where 0 is noon


	private RectTransform moveArea;
	private RectTransform playerIcon;

	private locationIcon curloc; //current location

	void Awake() {
		playerIcon = transform as RectTransform;
		RectTransform canvas = playerIcon;
		while (canvas.parent != null && canvas.parent is RectTransform) {
			canvas = canvas.parent as RectTransform;
		}
		moveArea = canvas;
	}

	void Update() {
		//move up/down/left/right but constrain to map
		Vector3 moveVec = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		timeofday = (timeofday + (int)moveVec.sqrMagnitude) % ticks_per_day;
		transform.Translate(moveVec * speed);
		ClampToMap();

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
