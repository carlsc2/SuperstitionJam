using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestMaster : Singleton<QuestMaster> {

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

    }

    /*void Start()
    {
        foreach(QuestBase quest in Quests){
            if (quest as QuestBase_Action)
            {
                if (quest.CharacterToAffect == QuestBase.charEnum.PLAYER)
                {
                    quest.AffectedCharacter = player;
                }
                if (quest.CharacterToAffect == QuestBase.charEnum.BOSS)
                {
                    quest.AffectedCharacter = boss;
                }
            }
            if (quest as QuestBase_Fetch)
            {
                if (quest.ItemToRecieve == QuestBase.ItemEnum.Item1)
                {
                    //add code to give item here
                }
            }
        }
    }*/

    protected QuestMaster() {}
    public GameObject player;
    //public GameObject boss;
    public List<QuestBase> Quests;
}
