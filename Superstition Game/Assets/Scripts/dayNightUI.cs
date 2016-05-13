using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class dayNightUI : MonoBehaviour {

	private Image img;

	public AnimationCurve daynightcurve;

	void Awake() {
		img = GetComponent<Image>();
	}

	void Update () {
		Color tmp = img.color;
		tmp.a = remap(TimeManager.timeofday,0, TimeManager.ticks_per_day,-1,1);
		tmp.a = daynightcurve.Evaluate((tmp.a < 0) ? -tmp.a : tmp.a) * 0.75f;
		img.color = tmp;

	}

	float remap(float value, float leftMin, float leftMax, float rightMin, float rightMax) {
		return rightMin + (value - leftMin) * (rightMax - rightMin) / (leftMax - leftMin);
	}
}
