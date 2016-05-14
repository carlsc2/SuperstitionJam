using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class locationIcon : MonoBehaviour {

	public static HashSet<string> visited_locations = new HashSet<string>();
	public string location_name;
	public string dest_scene;

	public GameObject namebox;

	void Awake() {
		//create title box, reveal if location is known
		if (visited_locations.Contains(location_name)) {
			namebox.SetActive(true);
		}
	}

	public void visit_location() {
		visited_locations.Add(location_name);
		namebox.SetActive(true);
	}

	public void enter() {
		SceneManager.LoadScene(dest_scene);
	}
}
