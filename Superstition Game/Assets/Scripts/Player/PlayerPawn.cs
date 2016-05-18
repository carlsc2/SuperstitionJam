using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerPawn : CharacterPawn {

    protected override void Awake() {
        base.Awake();

        DontDestroyOnLoad(gameObject);
    }

    protected override void Start() {
        base.Start();

        PlayerTracker_Singleton.Instance.player = this;
    }

    protected virtual void HandleFacingDirection(Direction faceDirec) {

    }

	
	
}
