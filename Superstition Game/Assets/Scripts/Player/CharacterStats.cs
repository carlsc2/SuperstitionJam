using UnityEngine;
using System.Collections;

public class CharacterStats : MonoBehaviour {

    public int health;
    public int maxHealth;
    public int strength;
    public int defense;
    public float speed;
    public float attackTime;


    public void DamageCharacter(float damageAmount) {
        //health = (Mathf.Clamp(health - damageAmount, 0.0f, Mathf.Infinity));
    }
}
