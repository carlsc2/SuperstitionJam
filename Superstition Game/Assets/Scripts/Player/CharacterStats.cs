using UnityEngine;
using System.Collections;

public class CharacterStats : MonoBehaviour {

    public enum Hand {
        Main = 0,
        Off = 1,
    }

    public float health;
    public float maxHealth;
    public float strength;
    public float defense;
    public float walkSpeed;
    public float accel;

    public float attackTime;

    public float dodgeTime;
    public float dodgeAccel;
    public float dodgeSpeed;

    public void ApplyDamage(float damageAmount) {
        health = (Mathf.Clamp(health - (defense - damageAmount), 0.0f, Mathf.Infinity));
    }


}
