using UnityEngine;
using System.Collections;

[RequireComponent(typeof(InventoryController))]
public class AIController : CharacterPawnController {

	public Transform targetTf;
	//CharacterPawn possessedPawn;
	public float chase_distance = 10f;

    protected override void Awake() {
        base.Awake();
    }

    // Use this for initialization
    protected override void Start () {
        base.Start();
        
		//possessedPawn = GetComponent<CharacterPawn>();
		//targetTf = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();

        if (possessedPawn == null) { return; }

		if (!targetTf) {//if we don't have a target, try to find one

            //if none can be found, do nothing
            if (!FindTarget()) { return; }
            else { Debug.Log(Vector3.Distance(targetTf.position, possessedPawn.transform.position)); }
        }


		if(Vector2.Distance(targetTf.position, possessedPawn.transform.position) < chase_distance) {
			Vector3 towardPlayer = targetTf.position - possessedPawn.transform.position;

            Debug.Log("chase after");

			if (towardPlayer.magnitude > 5) {
				Move();
			}
			else {
				int rand = Random.Range(0, 100);
				if (rand < 1)
					possessedPawn.Attack();
				if (rand > 80) {
					Move();
				}
			}
		}
		
	}

//FIND TARGET PROTOCOL
    protected virtual bool FindTarget() {
        if (PlayerTracker_Singleton.Instance == null) { return false; }
        if (PlayerTracker_Singleton.Instance.player == null) { return false; }

        //return false;

        targetTf = PlayerTracker_Singleton.Instance.player.transform;
        return true;
    }

	void Move()
	{
		Vector3 towardPlayer = targetTf.position - transform.position;

		float horizontal = 0;
		float vertical = 0;

		if (towardPlayer.x > 1)
			horizontal = 1;
		else if (towardPlayer.x < -1)
			horizontal = -1;

		if (towardPlayer.y > 1)
			vertical = 1;
		else if (towardPlayer.y < -1)
			vertical = -1;

		horizontal *= Random.Range(0f, 1);
		vertical *= Random.Range(0f, 1);

		//if (Random.Range(0, 100) > 80)
		//   horizontal *= 0;
		//if (Random.Range(0, 100) > 80)
		//    vertical *= 0;
		possessedPawn.Move(horizontal, vertical);
	}
}
