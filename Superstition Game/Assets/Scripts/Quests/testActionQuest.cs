using UnityEngine;
using System.Collections;

public class testActionQuest : QuestBase_Action {

    override public void CheckConditions(string str)
    {
        if (currentState == State.UKNOWN)
        {
            if (str == "talk")
            {
                currentState = State.STARTED;
            }
        }

        if (currentState == State.STARTED)
        {
            if (str == "killed")
            {
                NumberOfTimes--;
                if (NumberOfTimes == 0)
                {
                    currentState = State.COMPLETED;
                }
            }
        }

        if (currentState == State.COMPLETED)
        {
            if (str == "talk")
            {
                GiveReward();
            }
        }
    }

}
