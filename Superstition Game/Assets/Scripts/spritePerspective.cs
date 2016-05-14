using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class spritePerspective : MonoBehaviour {

	private SpriteRenderer spr;
	private List<SpriteRenderer> sprc;
	private Bounds maxbounds;

	void Awake() {
		sprc = new List<SpriteRenderer>();
	}

	void Start() {
		update_sprites();
	}

	public void update_sprites() {
		sprc.Clear();
		spr = GetComponent<SpriteRenderer>();
		foreach (SpriteRenderer sp in GetComponentsInChildren<SpriteRenderer>()) {
			sprc.Add(sp);
		}
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
