using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;


public class QuestBase : MonoBehaviour, ISerializationCallbackReceiver {

	public enum reward_type { ITEM, STAT_EFFECT };

	[Serializable]
	public class Reward_Object {
		public reward_type type;
		public string item_name;
		public CharacterStats.StatType stat_type;
		public float value;
		
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

	[SerializeField]
	private State journal_state = State.UKNOWN; //state of quest for journal purposes

	[SerializeField]
	private List<Quest_State> QuestStates; //list of all quest states in order

	[SerializeField]
	private Quest_State current_state;//current quest state
	private int state_index = -1;

	[SerializeField]
	private List<Reward_Object> QuestReward;

	[SerializeField]
	private Dictionary<string,int> current_parameters;


	[SerializeField]
	private Dictionary<string,string[]> _dialogues = new Dictionary<string, string[]>(); //maps each npc to their dialogue for each state

	[SerializeField]
	private List<NPC_Dialogue> NPC_Dialogues;

	public State get_journal_state() {
		return journal_state;
	}

	public string GetDialogue(string NPC_ID) {
		return _dialogues[NPC_ID][QuestStates.FindIndex(x => x.state_name == current_state.state_name)];
	}

	private void CheckConditions(){
		//check if state should be advanced

		//iterate through all parameters, checking
		foreach(Quest_Parameter qp in current_state.target_parameters) {
			if(current_parameters[qp.param_name] < qp.value) {
				//fail, don't set next state
				return;
			}
		}

		//set next state
		if (state_index < QuestStates.Count) {
			current_state = QuestStates[++state_index];
		}
		else {
			journal_state = State.COMPLETED;
		}
		if(state_index == 0) {
			journal_state = State.STARTED;
		}
	}

	public void SetParameter(string parameter) {
		//increments parameter and checks if state should transition
		if (!current_parameters.ContainsKey(parameter)) {
			current_parameters[parameter] = 0;
		}
		current_parameters[parameter] +=1;
		CheckConditions();
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

		//sanity check
		HashSet<string> paramnames = new HashSet<string>();
		foreach(Quest_State qs in QuestStates) {
			foreach(Quest_Parameter qp in qs.target_parameters) {
				if (paramnames.Contains(qp.param_name)) {
					Debug.LogError(string.Format("ERROR: parameter {0} in state {1} already in use. This will probably break some logic.",qp.param_name,qs.state_name));
				}
				else {
					paramnames.Add(qp.param_name);
				}
			}
		}
	}
}
