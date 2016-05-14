using UnityEngine;
using UnityEngine.UI;


public class ScrollTextFit : MonoBehaviour {

	private Text txt;
	private LayoutElement le;

	// Use this for initialization
	void Start () {
		txt = GetComponent<Text>();
		le = GetComponent<LayoutElement>();
	}
	
	// Update is called once per frame
	void Update () {
		le.preferredWidth = txt.preferredWidth *2;
	}
}
