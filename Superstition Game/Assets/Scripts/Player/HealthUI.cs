using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {

	Slider slider;
	public CharacterStats stats;

	// Use this for initialization
	void Start () 
	{
		//stats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
		slider = GetComponent<Slider>();
		slider.maxValue = stats.maxHealth;
	}
	
	// Update is called once per frame
	void Update () 
	{
		slider.value = stats.health;
	}
}
