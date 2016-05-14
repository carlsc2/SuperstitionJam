using UnityEngine;
using System.Collections;

public class AnimatorHandler : MonoBehaviour {

    public Animator anim;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual void Attack() { }

    public virtual void GetHit() { }


}
