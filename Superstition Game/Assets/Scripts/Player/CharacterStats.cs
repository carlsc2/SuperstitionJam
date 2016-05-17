using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CharacterStats : MonoBehaviour, ISerializationCallbackReceiver {

	[System.Serializable]
	public class StatBinder {
		[HideInInspector]
		public string name;
		[HideInInspector]
		public StatType type;
		public float value;
	}

	public enum StatType {
		Health,
		MaxHealth,
		Strength,
		Defense,
		MaxSpeed,
		Acceleration,
		DodgeTime,
		DodgeAcceleration,
		DodgeMaxSpeed,
	}

	[SerializeField]
	private List<StatBinder> statsList = new List<StatBinder>();

	public Dictionary<StatType, float> data = new Dictionary<StatType, float>();

	public void ApplyDamage(float damageAmount) {
		data[StatType.Health] = (Mathf.Clamp(data[StatType.Health] - (data[StatType.Defense] - damageAmount), 0.0f, Mathf.Infinity));
	}

	public void OnBeforeSerialize() {
		// save dictionary to list
		statsList.Clear();
		foreach (KeyValuePair<StatType, float> pair in data) {
			statsList.Add(new StatBinder() { name = pair.Key.ToString(), type = pair.Key, value = pair.Value });
		}

		foreach (StatType stat in System.Enum.GetValues(typeof(StatType))) {
			if (statsList.Where(x => x.type == stat).Count() == 0) {
				statsList.Add(new StatBinder() { name = stat.ToString(), type = stat, value = 0.0f });
			}
		}
	}


	public void OnAfterDeserialize() {
		// load dictionary from list
		data.Clear();
		foreach (StatBinder sb in statsList) {
			data.Add(sb.type, sb.value);
		}
	}
}
