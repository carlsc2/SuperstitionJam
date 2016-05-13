using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour {

	public static int day = 1;

	public static int ticks_per_day = 500; // number of move ticks per day

	public static int timeofday = 250; //make time go between 0 and ticks_per_day where 0 is noon


	public static void pass_time(int ticks) {
		timeofday = timeofday + ticks;
		day += timeofday / ticks_per_day;
		timeofday = timeofday % ticks_per_day;
	}

}
