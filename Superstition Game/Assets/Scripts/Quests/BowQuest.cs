using UnityEngine;
using System.Collections;

public class BowQuest : QuestBase_Action {

	public override void CheckConditions(string str) {

		/*if (currentState == State.UKNOWN) {
			if (str == "talk") {
				currentState = State.STARTED;
			}
		}

		if (currentState == State.STARTED) {
			if (str == "pull" || BowQuestLogic.broken) {
				currentState = State.COMPLETED;
			}
		}

		if (currentState == State.COMPLETED) {
			GiveReward();
		}*/
	}
}
