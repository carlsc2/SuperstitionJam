using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class spritePerspective : MonoBehaviour {

	private SpriteRenderer spr;
	private List<SpriteRenderer> sprc;
	private Bounds maxbounds;

	void Start() {
		spr = GetComponent<SpriteRenderer>();
		sprc = new List<SpriteRenderer>();
		foreach (SpriteRenderer sp in GetComponentsInChildren<SpriteRenderer>()) {
			sprc.Add(sp);
		}

		/*if(spr == null) {
			maxbounds = new Bounds();
			foreach(SpriteRenderer sp in sprc) {
				maxbounds.Encapsulate(sp.bounds);
			}
		}
		else {
			maxbounds = spr.bounds;
		}*/


	}

	void LateUpdate() {
		if (spr == null) {
			maxbounds = new Bounds();
			foreach (SpriteRenderer sp in sprc) {
				maxbounds.Encapsulate(sp.bounds);
			}
		}
		else {
			maxbounds = spr.bounds;
		}

		int SO = (int)Camera.main.WorldToScreenPoint(maxbounds.min).y * -1;
		if(spr != null) spr.sortingOrder = SO;
		foreach (SpriteRenderer sp in sprc) {
			sp.sortingOrder = SO;
		}
	}
}
