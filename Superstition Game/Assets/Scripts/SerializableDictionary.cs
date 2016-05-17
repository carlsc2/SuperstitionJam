using UnityEngine;
using System;
using System.Collections.Generic;


/*[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver {
	[SerializeField]
	private List<TKey> keys = new List<TKey>();

	[SerializeField]
	private List<TValue> values = new List<TValue>();

	// save the dictionary to lists
	public void OnBeforeSerialize() {
		keys.Clear();
		values.Clear();
		foreach (KeyValuePair<TKey, TValue> pair in this) {
			keys.Add(pair.Key);
			values.Add(pair.Value);
		}
	}

	// load dictionary from lists
	public void OnAfterDeserialize() {
		this.Clear();

		for (int i = 0; i < keys.Count; i++)
			this.Add(keys[i], values[i]);

		if (keys.Count != values.Count)
			throw new System.Exception(string.Format("there are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable.", keys.Count, values.Count));

		
	}
}*/


[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver {

	[Serializable]
	public class KVP {
		public TKey key;
		public TValue value;
		public KVP(TKey key, TValue value) {
			this.key = key;
			this.value = value;
		}
	}


	[SerializeField]
	public List<KVP> pairs = new List<KVP>();

	// save the dictionary to lists
	public void OnBeforeSerialize() {
		//pairs.Clear();
		foreach (KeyValuePair<TKey, TValue> pair in this) {
			pairs.Add(new KVP(pair.Key, pair.Value));
		}
	}

	// load dictionary from lists
	public void OnAfterDeserialize() {
		//this.Clear();

		for (int i = 0; i < pairs.Count; i++)
			this.Add(pairs[i].key, pairs[i].value);
	}
}