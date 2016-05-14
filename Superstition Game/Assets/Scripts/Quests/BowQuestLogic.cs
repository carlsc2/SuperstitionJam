using UnityEngine;
using System.Collections;

public class BowQuestLogic : MonoBehaviour, Interactable {

	public Sprite fixedsprite;
	public Sprite brokensprite;

	private SpriteRenderer spr;

	public static bool broken = false;

	void Awake() {
		spr = GetComponent<SpriteRenderer>();
		spr.sprite = broken ? brokensprite : fixedsprite;
	}

	virtual public void Interact(Transform t) {
		QuestMaster.Instance.Quests["BowQuest"].CheckConditions("pull");
		spr.sprite = brokensprite;
		broken = true;
	}
}
