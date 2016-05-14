using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestMaster : Singleton<QuestMaster> {

	public Dictionary<string,QuestBase> Quests = new Dictionary<string, QuestBase>();
	public static bool spawned = false;

	static QuestMaster(){
		prefab_name = "QuestMaster";
	}

	void Awake()
	{
		if (!spawned) {
			spawned = true;
		}

		DontDestroyOnLoad(gameObject);

		foreach (QuestBase qb in GetComponentsInChildren<QuestBase>()) {
			Quests[qb.questID] = qb;
		}

	}

}
