using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

    public GameObject player;
    public WorldSize ws;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (player)
        {
            transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            if (transform.position.x < 9.35f)
                transform.position = new Vector3(9.35f, transform.position.y, transform.position.z);
            else if (transform.position.x > (9.35f + 20.48f * (ws.numScreens - 1)))
                transform.position = new Vector3(9.35f + 20.48f * (ws.numScreens - 1), transform.position.y, transform.position.z);
        }
	}
}
