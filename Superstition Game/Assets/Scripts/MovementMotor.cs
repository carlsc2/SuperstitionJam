using UnityEngine;
using System.Collections;

public class MovementMotor : MonoBehaviour {

    CharacterStats stats;

    void Start()
    {
        stats = GetComponent<CharacterStats>();
    }

	public void Move(float horizontal, float vertical)
    {
        Vector2 dir = new Vector2(horizontal, vertical).normalized * stats.speed;
        transform.Translate(dir);
        
        //keep the player within the world boundaries
        if (transform.position.x > WorldBoundaries.maxX)
        {
            transform.position = new Vector2(WorldBoundaries.maxX, transform.position.y);
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
    }
}
