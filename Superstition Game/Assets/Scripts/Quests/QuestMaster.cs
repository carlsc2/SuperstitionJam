using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestMaster : Singleton<QuestMaster> {

	public Dictionary<string,QuestBase> Quests = new Dictionary<string, QuestBase>();

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		foreach(QuestBase qb in GetComponentsInChildren<QuestBase>()) {
			Quests[qb.questID] = qb;
		}

	}

}
