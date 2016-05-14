using UnityEngine;
using System.Collections;

public class makeSingleton : MonoBehaviour {

	public GameObject questfab;

	void Awake() {
		if (!QuestMaster.spawned) {
			Instantiate(questfab);
		}
	}
}
