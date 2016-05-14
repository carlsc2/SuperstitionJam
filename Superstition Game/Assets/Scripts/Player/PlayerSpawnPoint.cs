using UnityEngine;
using System.Collections;

public class PlayerSpawnPoint : MonoBehaviour {

    public GameObject playerPrefab;

	// Use this for initialization
	void Start () {
        GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");

        if (foundPlayer == null) {
            foundPlayer = (GameObject) GameObject.Instantiate(playerPrefab, transform.position, Quaternion.identity);
        }
        else {
            foundPlayer.transform.position = transform.position;
        }
        

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
