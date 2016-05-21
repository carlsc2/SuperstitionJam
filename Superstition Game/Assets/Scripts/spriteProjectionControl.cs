using UnityEngine;
using System.Collections.Generic;

public class spriteProjectionControl : MonoBehaviour {

	private Dictionary<Transform, Vector3> sprc = new Dictionary<Transform, Vector3>();
	// Use this for initialization
	void Awake () {
		foreach (SpriteRenderer sp in GetComponentsInChildren<SpriteRenderer>()) {
			sprc.Add(sp.transform,sp.transform.localRotation.eulerAngles);
		}
	}
	
	// Update is called once per frame
	void LateUpdate () {

		//the problem is ----> every bone has a different forward vector


		foreach(KeyValuePair<Transform,Vector3> kvp in sprc) {
			Vector3 orig = kvp.Value;
			Vector3 tmp = kvp.Key.localRotation.eulerAngles;
			tmp.x = orig.x;
			//tmp.z = orig.z;
			tmp.y = orig.y;
			kvp.Key.localRotation = Quaternion.Euler(tmp);
		}
	}
}
