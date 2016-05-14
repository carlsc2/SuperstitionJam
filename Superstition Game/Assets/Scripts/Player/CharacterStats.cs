using UnityEngine;
using System.Collections;

public class CharacterStats : MonoBehaviour {

    public float health;
    public float maxHealth;
    public float strength;
    public float defense;
    public float speed;
    public float attackTime;


    public void DamageCharacter(float damageAmount) {
        //health = (Mathf.Clamp(health - damageAmount, 0.0f, Mathf.Infinity));
    }
}
