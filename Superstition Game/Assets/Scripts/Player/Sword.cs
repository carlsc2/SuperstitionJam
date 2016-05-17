using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour {

	public CharacterStats stats;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Enemy" || other.tag == "Player")
		{
			CharacterStats otherStats = other.GetComponent<CharacterStats>();
			//CharacterStats stats = GetComponent<CharacterStats>();
			float damage = stats.data[CharacterStats.StatType.Strength] - stats.data[CharacterStats.StatType.Defense];
			if (damage > 0) {
				otherStats.ApplyDamage(damage);
			}
			if (otherStats.data[CharacterStats.StatType.Health] <= 0)
				Destroy(other.gameObject);
			else
			{
				other.GetComponent<PlayerPawn>().StartCoroutine(Flash(other.gameObject));
			}
		}
	}

	IEnumerator Flash(GameObject other)
	{
		SpriteRenderer[] renderers = other.GetComponentsInChildren<SpriteRenderer>();

		for (int i = 0; i < 2; i++)
		{
			foreach (SpriteRenderer sr in renderers)
			{
				sr.color = Color.red;
			}
			yield return new WaitForSeconds(.1f);

			foreach (SpriteRenderer sr in renderers)
			{
				sr.color = Color.white;
			}
			yield return new WaitForSeconds(.1f);
			
		}
	   
	}
}
