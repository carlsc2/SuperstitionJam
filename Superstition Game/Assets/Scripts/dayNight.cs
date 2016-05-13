using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class dayNight : MonoBehaviour {

	private Image img;

	void Awake() {
		img = GetComponent<Image>();
	}

	void Update () {
		Color tmp = img.color;
		tmp.a = remap(movePlayerIcon.timeofday,0,100,0,200)/200;
		img.color = tmp;
	}

	float remap(float value, float leftMin, float leftMax, float rightMin, float rightMax) {
		return rightMin + (value - leftMin) * (rightMax - rightMin) / (leftMax - leftMin);
	}
}
