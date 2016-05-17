using UnityEngine;
using System.Collections;

public class MovementMotor : MonoBehaviour {

	CharacterStats stats;
	Rigidbody2D rb;
	private WorldSize ws;

	public Vector2 desiredMoveDirec;
	public Vector2 trueMoveDirec;
	public float accel = 1.0f;

	public float speed;

	public bool isDodging = false;

	void Start()
	{
		stats = GetComponent<CharacterStats>();
		rb = GetComponent<Rigidbody2D>();
		ws = FindObjectOfType<WorldSize>();

		speed = stats.data[CharacterStats.StatType.MaxSpeed];
	}

	void Update()
	{
		//rb.velocity = Vector3.zero;

		ActuallyMove();

	}

	private void ActuallyMove() {
		//if (!isDodging) {
			trueMoveDirec = Vector3.MoveTowards(trueMoveDirec, desiredMoveDirec, accel);
		//}

		//Vector3 appliedMoveDirec = trueMoveDirec * speed;


		/*
		//keep the player within the world boundaries
		if (transform.position.x + appliedMoveDirec.x > WorldBoundaries.maxX + WorldBoundaries.width * (ws.numScreens - 1)) {
			//transform.position = new Vector2(WorldBoundaries.maxX + WorldBoundaries.width * (ws.numScreens - 1), transform.position.y);

			//adjust the applied direction against the boundary
			appliedMoveDirec.x += (WorldBoundaries.maxX + WorldBoundaries.width * (ws.numScreens - 1)) - (transform.position.x + appliedMoveDirec.x);
		}
		else if (transform.position.x + appliedMoveDirec.x < WorldBoundaries.minX) {
			//transform.position = new Vector2(WorldBoundaries.minX, transform.position.y);

			appliedMoveDirec.x += WorldBoundaries.minX - (transform.position.x + appliedMoveDirec.x);
		}

		if (transform.position.y + appliedMoveDirec.y > WorldBoundaries.maxY) {
			//transform.position = new Vector2(transform.position.x, WorldBoundaries.maxY);

			appliedMoveDirec.y += WorldBoundaries.maxY - (transform.position.y + appliedMoveDirec.y);

		}
		else if (transform.position.y + appliedMoveDirec.y < WorldBoundaries.minY) {
			//transform.position = new Vector2(transform.position.x, WorldBoundaries.minY);
			appliedMoveDirec.y += WorldBoundaries.minY 
		}
		*/
		rb.velocity = trueMoveDirec * speed; //appliedMoveDirec;
	}

	public void Move(float horizontal, float vertical)
	{

		if (isDodging) { return; }

		desiredMoveDirec = new Vector2(horizontal, vertical).normalized;



		/*
		moveDirec.x = horizontal;
		moveDirec.y = vertical;
		moveDirec.Normalize();

		Vector2 dir = new Vector2(horizontal, vertical).normalized * stats.speed;
		rb.MovePosition(rb.position + dir);
		
		//keep the player within the world boundaries
		if (transform.position.x > WorldBoundaries.maxX + WorldBoundaries.width * (ws.numScreens - 1))
		{
			transform.position = new Vector2(WorldBoundaries.maxX + WorldBoundaries.width * (ws.numScreens - 1), transform.position.y);
		}
		else if (transform.position.x < WorldBoundaries.minX)
		{
			transform.position = new Vector2(WorldBoundaries.minX, transform.position.y);
		}

		if (transform.position.y > WorldBoundaries.maxY)
		{
			transform.position = new Vector2(transform.position.x, WorldBoundaries.maxY);
		}
		else if (transform.position.y < WorldBoundaries.minY)
		{
			transform.position = new Vector2(transform.position.x, WorldBoundaries.minY);
		}
		*/
	}
}
