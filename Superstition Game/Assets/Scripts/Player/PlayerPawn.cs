using UnityEngine;
using System.Collections;

public class PlayerPawn : Pawn {

    enum state { idle, walk, attack, defend };
    enum direction { left, right }
    state currentState = state.idle;
    direction facing = direction.left;
    MovementMotor mm;
    CharacterStats stats;

    //SpriteRenderer sr;

    public float defendModifier = 3f;

    public GameObject swordHitbox;
    public GameObject shieldHitbox;

    void Start()
    {
        mm = GetComponent<MovementMotor>();
        //sr = GetComponent<SpriteRenderer>();
        stats = GetComponent<CharacterStats>();
    }

    void Update()
    {
        /*if (currentState == state.idle)
            sr.color = Color.white;
        else if (currentState == state.walk)
            sr.color = Color.green;
        else if (currentState == state.attack)
            sr.color = Color.red;
        else if (currentState == state.defend)
            sr.color = Color.blue;*/
    }

	public override void Attack()
    {
        if (currentState != state.attack && currentState != state.defend)
        {
            currentState = state.attack;
            swordHitbox.SetActive(true);
            Invoke("EndAttack", stats.attackTime);
        }
    }

    void EndAttack()
    {
        currentState = state.idle;
        swordHitbox.SetActive(false);
    }

    public override void Defend()
    {
        if(currentState != state.attack && currentState != state.defend)
        {
            currentState = state.defend;
            shieldHitbox.SetActive(true);
            stats.speed /= defendModifier;
        }
    }

    public override void EndDefend()
    {
        if(currentState == state.defend)
        {
            currentState = state.idle;
            shieldHitbox.SetActive(false);
            stats.speed *= defendModifier;
        }
    }

    public override void Interact()
    {
        //interaction stuff goes here
    }

    public override void Move(float horizontal, float vertical)
    {
        if (currentState != state.attack)
        {
            if (currentState == state.defend)
            {
                mm.Move(horizontal, vertical);
            }
            else
            {
                mm.Move(horizontal, vertical);
                if (horizontal > 0)
                {
                    facing = direction.right;
                    transform.localScale = Vector3.one;
                }
                else if (horizontal < 0)
                {
                    facing = direction.left;
                    transform.localScale = new Vector3(-1, 1, 1);
                }


                if (currentState != state.walk)
                    currentState = state.walk;
            }
        }
    }

    public override void Idle()
    {
        if (currentState != state.attack && currentState != state.defend && currentState != state.idle)
        {
            currentState = state.idle;
        }
    }
}
