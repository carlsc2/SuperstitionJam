using UnityEngine;
using System.Collections;

public class CharacterStats : MonoBehaviour {

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
