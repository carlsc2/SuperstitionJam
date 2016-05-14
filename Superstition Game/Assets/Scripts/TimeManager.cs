using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour {

	public static int day = 1;

	public static int ticks_per_day = 500; // number of move ticks per day

	public static int timeofday = 250; //make time go between 0 and ticks_per_day where ticks_per_day/2 is noon

	public static int days_until_fight = 12;


	public static void pass_time(int ticks) {
		timeofday = timeofday + ticks;
		day += timeofday / ticks_per_day;
		timeofday = timeofday % ticks_per_day;

		if(day > days_until_fight) {
			SceneManager.LoadScene("finalfight");
		}
	}

}
