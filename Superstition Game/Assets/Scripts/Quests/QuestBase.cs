using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;


public class QuestBase : MonoBehaviour, ISerializationCallbackReceiver {

	public enum reward_type { ITEM, STAT_EFFECT };

	[Serializable]
	public class Reward_Object {
		public reward_type type;
		public float value;
		public string item_name;
	}

	[Serializable]
	public class Quest_Parameter {
		public string param_name;
		public int value;
	}

	[Serializable]
	public class Quest_State {
		public string state_name;
		public Quest_Parameter[] target_parameters;
	}	

	[Serializable]
	public class NPC_Dialogue {
		public string NPC_ID;
		public string[] dialogue_strings;

		public NPC_Dialogue(string NPC_ID, string[] dialogue_strings) {
			this.NPC_ID = NPC_ID;
			this.dialogue_strings = dialogue_strings;
		}
	}

	//[Serializable]
	//public class DialogueDictionary : SerializableDictionary<string, string> { }

	public string questID;

	public enum State { UKNOWN, STARTED, COMPLETED };
	public State journal_state = State.UKNOWN; //state of quest for journal purposes

	[SerializeField]
	private List<Quest_State> QuestStates; //list of all quest states in order

	[SerializeField]
	private List<Quest_Parameter> QuestParams; //list of all quest parameters

	[SerializeField]
	private Quest_State current_state;//current quest state

	[SerializeField]
	private List<Reward_Object> QuestReward;


	[SerializeField]
	private Dictionary<string,string[]> _dialogues = new Dictionary<string, string[]>(); //maps each npc to their dialogue for each state

	[SerializeField]
	private List<NPC_Dialogue> NPC_Dialogues;

	public string GetDialogue(string NPC_ID) {
		return _dialogues[NPC_ID][QuestStates.FindIndex(x => x.state_name == current_state.state_name)];
	}

	virtual public void CheckConditions(string str){
		//check if state should be advanced

		//iterate through all parameters, checking





	}

	public void set_parameter(string name, int value) {

	}

	public void GiveReward() {
		foreach (Reward_Object rw in QuestReward) {
			if(rw.type == reward_type.ITEM) {
				//add item to inventory
				//use reward.item_name;
			}else if (rw.type == reward_type.STAT_EFFECT) {
				//apply stat effect to player
				//use rw.value
			}
		}
	}

	
	public void OnBeforeSerialize() {
		// save dictionary to list
		NPC_Dialogues.Clear();
		foreach (KeyValuePair<string, string[]> pair in _dialogues) {
			NPC_Dialogues.Add(new NPC_Dialogue(pair.Key, pair.Value));		
		}
	}

	
	public void OnAfterDeserialize() {
		// load dictionary from list
		_dialogues.Clear();
		foreach(NPC_Dialogue nd in NPC_Dialogues) {
			if (nd.dialogue_strings.Length != QuestStates.Count) {
				Array.Resize(ref nd.dialogue_strings, QuestStates.Count);
			}
			try {
				_dialogues.Add(nd.NPC_ID, nd.dialogue_strings);
			}catch (ArgumentException) {
				_dialogues.Add(nd.NPC_ID + "_1", nd.dialogue_strings);
			}

		}
	}
}
