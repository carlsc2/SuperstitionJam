using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerPawn : CharacterPawn {

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

	private Vector3 oscale;

	
	protected override void Awake() {
		base.Awake();

		motor = GetComponent<MovementMotor>();
		inventory = GetComponent<InventoryController>();
		anim = GetComponent<AnimatorHandler>();
		//audSource = GetComponent<AudioSource>();

		//sr = GetComponent<SpriteRenderer>();
		//stats = GetComponent<CharacterStats>();
		interactables = new HashSet<Transform>();
		oscale = transform.localScale;

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

			stats.data[CharacterStats.StatType.MaxSpeed] /= defendModifier;
		}
	}

	public override void EndDefend()
	{
		if(currentState == state.defend)
		{
			currentState = state.idle;
			//shieldHitbox.SetActive(false);
			inventory.EndUseItemInHand(InventoryController.Hand.Off);

			stats.data[CharacterStats.StatType.MaxSpeed] *= defendModifier;
		}
	}

	public override void Interact()
	{
		//interact with nearest thing
		Transform nearest = null;
		float min_dist = Mathf.Infinity;
		Vector3 current_pos = transform.position;
		foreach (Transform t in interactables) {
			if (t == null) { continue; }

			float dist = Vector3.Distance(t.position, current_pos);
			if (dist < min_dist) {
				nearest = t;
				min_dist = dist;
			}
		}
		if(nearest != null) {
			nearest.GetComponentInChildren<Interactable>().Interact(transform);

			anim.Interact();
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
					transform.localScale = new Vector3(oscale.x, oscale.y, oscale.z);
				}
				else if (horizontal < 0)
				{
					facing = direction.left;
					transform.localScale = new Vector3(-oscale.x, oscale.y, oscale.z);
				}

				//ANIMATION
				anim.SetWalkBlend(motor.trueMoveDirec.magnitude);

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

	public override void Dodge() {
		StartCoroutine(DodgeTimer(stats.data[CharacterStats.StatType.DodgeTime],
								  stats.data[CharacterStats.StatType.DodgeMaxSpeed],
								  stats.data[CharacterStats.StatType.DodgeAcceleration]));
	}

	private IEnumerator DodgeTimer(float time, float speed, float accel) {

		motor.isDodging = true;
		motor.accel = accel;
		motor.speed = speed;
		motor.trueMoveDirec = motor.desiredMoveDirec.normalized * speed;

		yield return new WaitForSeconds(time);


		motor.isDodging = false;
		motor.accel = stats.data[CharacterStats.StatType.Acceleration];
		motor.speed = stats.data[CharacterStats.StatType.MaxSpeed];

		yield return null;
	}

	public override void SelectItemFromInventory(int hotbarNumber) {

		inventory.EquipHotbarItem(hotbarNumber);

	}

	public override void KillPawn() {
		base.KillPawn();

		//Trigger death animation on the model
		anim.TriggerDeath();
		


		//detatch the model from the GameObject that drives logic
		rig.transform.parent = null;

		//Destroy this gameobject
		Destroy(gameObject);

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
