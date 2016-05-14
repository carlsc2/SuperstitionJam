using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	private Transform player;
	private WorldSize ws;

	// Use this for initialization
	void Start () 
	{
		ws = FindObjectOfType<WorldSize>();
        GetPlayer();

        player = PlayerController.singleton.transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (player)
		{
			transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
			if (transform.position.x < 9.35f)
				transform.position = new Vector3(9.35f, transform.position.y, transform.position.z);
			else if (transform.position.x > (9.35f + 20.48f * (ws.numScreens - 1)))
				transform.position = new Vector3(9.35f + 20.48f * (ws.numScreens - 1), transform.position.y, transform.position.z);
		}
        else {
            GetPlayer();
        }
	}

    private void GetPlayer () {
        /*
        Transform temp = GameObject.FindGameObjectWithTag("Player").transform;

        if (temp != null) {
            player = temp;
        }
        */
    }
}
