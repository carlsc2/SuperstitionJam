using UnityEngine;
using System.Collections;

public class MovementMotor : MonoBehaviour {

    CharacterStats stats;

    void Start()
    {
        stats = GetComponent<CharacterStats>();
    }

	public void Move(float horizontal, float vertical)
    {
        transform.Translate(horizontal * stats.speed, vertical * stats.speed, 0);
    }
}
