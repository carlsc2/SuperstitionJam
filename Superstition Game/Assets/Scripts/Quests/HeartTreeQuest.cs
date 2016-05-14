using UnityEngine;
using System.Collections;

public class HeartTreeQuest : QuestBase_Action {

	/* Quest description:
			talk to dude
			go to tree
			dance
			get


	*/

	public ParticleSystem ps;

	public override void CheckConditions(string str) {

		if (currentState == State.UKNOWN) {
			if (str == "talk") {
				currentState = State.STARTED;
			}
		}

		if (currentState == State.STARTED) {
			if (str == "dance") {
				NumberOfTimes--;
				if (NumberOfTimes <= 0) {
					currentState = State.COMPLETED;
				}
			}
		}

		if (currentState == State.COMPLETED) {
			GiveReward();
			ps.Play();
		}
	}
}
