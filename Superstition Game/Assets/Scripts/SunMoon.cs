using UnityEngine;
using System.Collections;

public class SunMoon : MonoBehaviour {

	public Transform sun;
	public Transform moon;

	public float radius = 10f;

	public float acceleration = 60f; //how many times faster than real life?

	private float accumulated_ticks = 0;

	void Start() {
		float t = remap(TimeManager.timeofday, 0, TimeManager.ticks_per_day, 0, 2 * Mathf.PI);


		float mx = radius * Mathf.Cos(t + Mathf.PI / 2);
		float my = radius * Mathf.Sin(t + Mathf.PI / 2);

		float sx = radius * Mathf.Cos(t + 1.5f * Mathf.PI);
		float sy = radius * Mathf.Sin(t + 1.5f * Mathf.PI);

		sun.position = new Vector3(sx, sy, 0) + transform.position;
		moon.position = new Vector3(mx, my, 0) + transform.position;

	}

	void Update() {

		//sun should be at highest point when timeofday = 0
		//moon should be opposite

		accumulated_ticks += remap(Time.deltaTime * acceleration, 0, 86400, 0, TimeManager.ticks_per_day);
		if (accumulated_ticks > 1) {
			TimeManager.pass_time(1);
			accumulated_ticks = accumulated_ticks % 1;
		}

		float t = remap(TimeManager.timeofday + accumulated_ticks, 0, TimeManager.ticks_per_day, 0, 2*Mathf.PI);


		float mx = radius * Mathf.Cos(t + Mathf.PI/2);
		float my = radius * Mathf.Sin(t + Mathf.PI/2);

		float sx = radius * Mathf.Cos(t + 1.5f * Mathf.PI);
		float sy = radius * Mathf.Sin(t + 1.5f * Mathf.PI);

		Vector3 sun_position = new Vector3(sx, sy, 0) + transform.position;
		Vector3 moon_position = new Vector3(mx, my, 0) + transform.position;

		sun.position = Vector3.MoveTowards(sun.position, sun_position, Time.deltaTime);
		moon.position = Vector3.MoveTowards(moon.position, moon_position, Time.deltaTime);

	}

	float remap(float value, float leftMin, float leftMax, float rightMin, float rightMax) {
		return rightMin + (value - leftMin) * (rightMax - rightMin) / (leftMax - leftMin);
	}
}
