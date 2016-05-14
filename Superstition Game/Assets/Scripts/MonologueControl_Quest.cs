using UnityEngine;
using System.Collections;

public class MonologueControl_Quest : MonologueControl {
	public string questID;
	private QuestBase quest;

	public string[] completionDialogue;

	public enum QuestType { FETCH, ACTION };
	public QuestType QType;

	void Start() {
		quest = QuestMaster.Instance.Quests[questID];
	}

	override public void Interact(Transform t)
	{
		Debug.Log("in interact");
		if (QType == QuestType.ACTION)
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
		else if (QType == QuestType.FETCH)
		{
			Debug.Log(quest.Inventory.itemsInInventory.Count);
			if (quest.Inventory.HasItemOfId(quest.QuestItem.id))
			{
				Debug.Log("in FETCH");

				chat_dialogue = completionDialogue;

				is_chatting = true;
				is_idle = false;
				is_speaking = false;

				quest.CheckConditions("givereward");
			}
			/*foreach (InventoryController.ItemSlot itemslot in quest.Inventory.itemsInInventory)
			{
				print("hello");
				if (quest.QuestItem.id == itemslot.item.id)
				{
					Debug.Log("in FETCH");

					chat_dialogue = completionDialogue;

					is_chatting = true;
					is_idle = false;
					is_speaking = false;

					quest.CheckConditions("givereward");
				}
			}*/
		}
	}
}
