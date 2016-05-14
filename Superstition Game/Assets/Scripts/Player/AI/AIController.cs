using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {

    public GameObject player;
    Pawn p;

	// Use this for initialization
	void Start () 
    {
        p = GetComponent<Pawn>();
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!player)
            return;
        Vector3 towardPlayer = player.transform.position - transform.position;

        float horizontal = 0;
        float vertical = 0;

        if (towardPlayer.magnitude > 5)
        {
            if (towardPlayer.x > 1)
                horizontal = 1;
            else if (towardPlayer.x < -1)
                horizontal = -1;

            if (towardPlayer.y > 1)
                vertical = 1;
            else if (towardPlayer.y < -1)
                vertical = -1;

            p.Move(horizontal, vertical);
        }
        else
        {
            p.Attack();
        }
	}
}
