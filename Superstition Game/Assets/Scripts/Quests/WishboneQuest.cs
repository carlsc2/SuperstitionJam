using UnityEngine;
using System.Collections;

public class WishboneQuest : QuestBase_Fetch
{
    override public void CheckConditions(string str)
    {
        if (currentState != State.TURNED_IN)
        {
            if (str == "givereward")
            {

                GiveReward();

            }
        }
    }
}
