using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class displayDays : MonoBehaviour {

	private Text txt;

	// Use this for initialization
	void Awake () {
		txt = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		txt.text = "Day " + TimeManager.day;
	}
}
