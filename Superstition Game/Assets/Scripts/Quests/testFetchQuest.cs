using UnityEngine;
using System.Collections;

public class testFetchQuest : QuestBase_Fetch{

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
            if (str == "pickup")
            {
                   currentState = State.COMPLETED;
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
