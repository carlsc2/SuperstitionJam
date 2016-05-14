using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class MonologueControl : MonoBehaviour {


	public RectTransform chatbox;
	private RectTransform canvas;
	private Text txt;

	private float chat_height_offset;

	private IEnumerator chatroutine;

	void Awake() {
		canvas = chatbox;
		while (canvas.parent != null && canvas.parent is RectTransform) {
			canvas = canvas.parent as RectTransform;
		}
		txt = chatbox.GetComponentInChildren<Text>();

		chat_height_offset = GetComponent<SpriteRenderer>().bounds.size.y / 2;

	}

	void Update () {
		
		
	}

	public void speak_words(string words) {
		if(chatroutine != null) {
			StopCoroutine(chatroutine);
		}
		chatroutine = _speak_words(words);
		StartCoroutine(chatroutine);
	}

	IEnumerator _speak_words(string words) {
		//make chat appear above head and fade out after a time

		txt.text = words;

		float duration = 3 + words.Length/7f;

		float start_time = Time.time;

		//make chat box visible
		chatbox.gameObject.SetActive(true);

		while(Time.time < duration + start_time) {

			//update position of chat box

			Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(transform.position + Vector3.up * chat_height_offset);

			Vector2 WorldObject_ScreenPosition = new Vector2(
			((ViewportPosition.x * canvas.sizeDelta.x) - (canvas.sizeDelta.x * 0.5f)),
			((ViewportPosition.y * canvas.sizeDelta.y) - (canvas.sizeDelta.y * 0.5f)));

			chatbox.anchoredPosition = WorldObject_ScreenPosition;

			yield return null;
		}

		//hide chat box
		chatbox.gameObject.SetActive(false);



	}


}
