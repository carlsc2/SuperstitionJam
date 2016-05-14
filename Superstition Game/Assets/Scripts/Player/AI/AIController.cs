using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour {

    public GameObject player;
    Pawn p;

	// Use this for initialization
	void Start () 
    {
        p = GetComponent<Pawn>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
