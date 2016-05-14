using UnityEngine;
using System.Collections;

public class HeartTreeLogic : MonoBehaviour {

	bool player_in_range = false;
	PlayerPawn player;
	private int boops = 0;
	private bool toggle = false;
	private HeartTreeQuest htq;

	void Start() {
		htq = QuestMaster.Instance.GetComponentInChildren<HeartTreeQuest>();
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.transform.root.tag == "Player") {
			player = col.transform.root.GetComponent<PlayerPawn>();
			player_in_range = true;
			htq.AffectedCharacter = player.transform.root.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.transform.root.tag == "Player") {
			player_in_range = false;
		}
	}

	void Update() {
		if (player_in_range) {
			if(!toggle && player.currentState == PlayerPawn.state.attack) {
				boops++;
				htq.CheckConditions("dance");
				toggle = true;
			}
			else if(toggle && player.currentState != PlayerPawn.state.attack) {
				toggle = false;
			}
		}
	}
}
