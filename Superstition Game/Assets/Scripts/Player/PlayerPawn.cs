using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerPawn : Pawn {

	public enum state { idle, walk, attack, defend };
	enum direction { left, right }
	public state currentState = state.idle;
	direction facing = direction.left;
	MovementMotor motor;
	InventoryController inventory;
	AnimatorHandler anim;
	//CharacterStats stats;

	//SpriteRenderer sr;

	public float defendModifier = 3f;

	//public GameObject swordHitbox;
	//public GameObject shieldHitbox;

	private HashSet<Transform> interactables;

	protected override void Awake() {
		base.Awake();

		motor = GetComponent<MovementMotor>();
		inventory = GetComponent<InventoryController>();
		anim = GetComponent<AnimatorHandler>();

		//sr = GetComponent<SpriteRenderer>();
		//stats = GetComponent<CharacterStats>();
		interactables = new HashSet<Transform>();
	}

	protected override void Start()
	{
		base.Start();
		//swordHitbox.SetActive(false);
		//shieldHitbox.SetActive(false);

	}

	protected override void Update()
	{
		base.Update();

		/*if (currentState == state.idle)
			sr.color = Color.white;
		else if (currentState == state.walk)
			sr.color = Color.green;
		else if (currentState == state.attack)
			sr.color = Color.red;
		else if (currentState == state.defend)
			sr.color = Color.blue;*/
	}

	public override void Attack()
	{
		if (currentState != state.attack && currentState != state.defend)
		{
			currentState = state.attack;
			//swordHitbox.SetActive(true);
			inventory.BeginUseItemInHand(InventoryController.Hand.Main);

			Invoke("EndAttack", stats.attackTime);
		}
	}

	void EndAttack()
	{
		currentState = state.idle;

		inventory.EndUseItemInHand(InventoryController.Hand.Main);

		//swordHitbox.SetActive(false);
	}

	public override void Defend()
	{
		if(currentState != state.attack && currentState != state.defend)
		{
			currentState = state.defend;
			//shieldHitbox.SetActive(true);
			//inventory.offHandItem.BeginUseItem();
			inventory.BeginUseItemInHand(InventoryController.Hand.Off);

			stats.speed /= defendModifier;
		}
	}

	public override void EndDefend()
	{
		if(currentState == state.defend)
		{
			currentState = state.idle;
			//shieldHitbox.SetActive(false);
			inventory.EndUseItemInHand(InventoryController.Hand.Off);

			stats.speed *= defendModifier;
		}
	}

	public override void Interact()
	{
		//interact with nearest thing
		Transform nearest = null;
		float min_dist = Mathf.Infinity;
		Vector3 current_pos = transform.position;
		foreach (Transform t in interactables) {
			float dist = Vector3.Distance(t.position, current_pos);
			if (dist < min_dist) {
				nearest = t;
				min_dist = dist;
			}
		}
		if(nearest != null) {
			nearest.GetComponentInChildren<Interactable>().Interact(transform);
		}
	}

	public override void Move(float horizontal, float vertical)
	{
		if (currentState != state.attack)
		{
			if (currentState == state.defend)
			{
				motor.Move(horizontal, vertical);
			}
			else
			{
				motor.Move(horizontal, vertical);
				if (horizontal > 0)
				{
					facing = direction.right;
					transform.localScale = new Vector3(.5f, .5f, 1);
				}
				else if (horizontal < 0)
				{
					facing = direction.left;
					transform.localScale = new Vector3(-.5f, .5f, 1);
				}

				//ANIMATION
				anim.SetWalkBlend(motor.delayedMoveDirec.magnitude);

				if (currentState != state.walk)
					currentState = state.walk;
			}
		}
	}

	public override void Idle()
	{
		if (currentState != state.attack && currentState != state.defend && currentState != state.idle)
		{
			currentState = state.idle;
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		//update interaction list
		Interactable tmp = col.transform.root.GetComponentInChildren<Interactable>();
		if(tmp != null) {
			interactables.Add(col.transform.root);
		}	
	}

	void OnTriggerExit2D(Collider2D col) {
		//update interaction list
		interactables.Remove(col.transform.root);
	}
	
}
