using UnityEngine;
using System.Collections;

public class Pawn : MonoBehaviour {

    public virtual void Attack()
    { }

    public virtual void Defend()
    { }

    public virtual void EndDefend()
    { }

    public virtual void Move(float horizontal, float vertical)
    { }

    public virtual void Idle()
    { }

    public virtual void Interact()
    { }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy" || other.tag == "Player")
        {
            CharacterStats otherStats = other.GetComponent<CharacterStats>();
            CharacterStats stats = GetComponent<CharacterStats>();


            otherStats.health -= (stats.strength - otherStats.defense);
            if (otherStats.health <= 0)
                Destroy(other.gameObject);
            else
                StartCoroutine(Flash(other.gameObject));
        }
    }

    IEnumerator Flash(GameObject other)
    {
        SpriteRenderer sr = other.GetComponent<SpriteRenderer>();

        for(int i = 2; i != 0; i--)
        {
            if (sr)
            {
                sr.color = Color.red;
                yield return new WaitForSeconds(.1f);
            }
            if (sr)
            {
                sr.color = Color.white;
                yield return new WaitForSeconds(.1f);
            }
        }
    }
}
