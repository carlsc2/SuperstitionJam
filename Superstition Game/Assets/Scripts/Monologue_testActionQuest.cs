using UnityEngine;
using System.Collections;

public class Monologue_testActionQuest : MonologueControl {
    public QuestBase quest;

    override public void Interact(Transform t)
    {
        is_chatting = true;
        is_idle = false;
        is_speaking = false;
        quest.CheckConditions("talk");
    }
}
