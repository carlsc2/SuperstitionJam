using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class spritePerspective : MonoBehaviour {

	private SpriteRenderer spr;
	private int sproffset;
	private List<SpriteRenderer> sprc = new List<SpriteRenderer>();
	private Bounds maxbounds;

	private Dictionary<int, int> offsets = new Dictionary<int, int>();//map list index : sorting order

	void Start() {
		update_sprites();
	}

	public void update_sprites() {
		//reset sorting order
		for (int i = 0; i < sprc.Count; i++) {
			sprc[i].sortingOrder = offsets[i];
		}
		sprc.Clear();
		spr = GetComponent<SpriteRenderer>();
		if (spr != null) sproffset = spr.sortingOrder;
		foreach (SpriteRenderer sp in GetComponentsInChildren<SpriteRenderer>()) {
			sprc.Add(sp);
		}
		for(int i=0; i < sprc.Count; i++) {
			offsets[i] = sprc[i].sortingOrder;
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
		if(spr != null) spr.sortingOrder = SO + sproffset;
		for (int i = 0; i < sprc.Count; i++) {
			sprc[i].sortingOrder = SO + offsets[i];
		}
	}
}
