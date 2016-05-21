using UnityEngine;
using System.Collections;

public class Pawn : MonoBehaviour {

    public CharacterPawnController owningController;

    protected virtual void Awake() { }

	// Use this for initialization
	protected virtual void Start () {
	
	}
	
	// Update is called once per frame
	protected virtual void Update () {
	
	}

    protected virtual void OnDestroy() {
        //unhook when Pawn gets destroyed
        DisconnectPawnFromController();
    }

    public virtual void DisconnectPawnFromController() {
        if (owningController == null) { return; }

        owningController.possessedPawn = null;
    }
}
