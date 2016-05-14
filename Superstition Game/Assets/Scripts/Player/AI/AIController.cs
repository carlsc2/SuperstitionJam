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

            horizontal *= Random.Range(0f, 1);
            vertical *= Random.Range(0f, 1);

            //if (Random.Range(0, 100) > 80)
            //   horizontal *= 0;
            //if (Random.Range(0, 100) > 80)
            //    vertical *= 0;
            p.Move(horizontal, vertical);
        }
        else
        {
            int rand = Random.Range(0, 100);
            if(rand < 1)
                p.Attack();
        }
	}
}
