using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu]
public class QuestMaster : Singleton<QuestMaster> {
    protected QuestMaster() {}

    public List<QuestBase> Quests;
}
