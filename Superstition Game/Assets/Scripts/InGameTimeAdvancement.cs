using UnityEngine;
using System.Collections;

public class InGameTimeAdvancement : MonoBehaviour {

	public float acceleration = 60f; //how many times faster than real life?

	private float accumulated_ticks = 0;

	// Update is called once per frame
	void Update () {
		accumulated_ticks += remap(Time.deltaTime * acceleration, 0, 86400, 0, TimeManager.ticks_per_day);
		if(accumulated_ticks > 1) {
			TimeManager.pass_time(1);
			accumulated_ticks = accumulated_ticks % 1;
		}
	}

	float remap(float value, float leftMin, float leftMax, float rightMin, float rightMax) {
		return rightMin + (value - leftMin) * (rightMax - rightMin) / (leftMax - leftMin);
	}
}
