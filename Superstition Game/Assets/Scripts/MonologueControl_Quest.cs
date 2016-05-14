using UnityEngine;
using System.Collections;

public class MonologueControl_Quest : MonologueControl {
    public QuestBase quest;
    public string[] completionDialogue;

    override public void Interact(Transform t)
    {
        if (quest.currentState == QuestBase.State.COMPLETED || quest.currentState == QuestBase.State.TURNED_IN)
        {
            chat_dialogue = completionDialogue;
        }

        is_chatting = true;
        is_idle = false;
        is_speaking = false;
        
        quest.CheckConditions("talk");
    }
}
