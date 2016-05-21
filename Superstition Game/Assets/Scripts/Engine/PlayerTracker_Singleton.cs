using UnityEngine;
using System.Collections;

public class PlayerTracker_Singleton : Singleton<PlayerTracker_Singleton> {

    void Awake() {
        DontDestroyOnLoad(gameObject);
    }

	protected PlayerTracker_Singleton() { }

    public PlayerPawn player;
}
