using UnityEngine;
using System.Collections;

public class MovementMotor : MonoBehaviour {

    CharacterStats stats;
    Rigidbody2D rb;
    public WorldSize ws;

    void Start()
    {
        stats = GetComponent<CharacterStats>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = Vector3.zero;
    }

    public void Move(float horizontal, float vertical)
    {
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
    }
}
