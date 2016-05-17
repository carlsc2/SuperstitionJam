using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {

	Slider slider;
	[HideInInspector]
	public CharacterStats stats;
	

	// Use this for initialization
	void Start () 
	{
		if (transform.root.tag == "Enemy"  )
		{
			stats = GetComponentInParent<CharacterStats>();
		}
		else {
			//stats = PlayerController.singleton.GetComponent<CharacterStats>();
			stats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
		}
   
		slider = GetComponent<Slider>();
		slider.maxValue = stats.data[CharacterStats.StatType.MaxHealth];
	}
	
	// Update is called once per frame
	void Update () 
	{
		slider.value = stats.data[CharacterStats.StatType.Health];
	}
}
