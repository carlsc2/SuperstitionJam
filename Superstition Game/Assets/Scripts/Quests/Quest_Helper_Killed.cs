using UnityEngine;
using System.Collections;

public class Quest_Helper_Killed : MonoBehaviour {

    public QuestBase quest;
	public void OnDestroy(){
        quest.CheckConditions("killed");
    }
	
}
