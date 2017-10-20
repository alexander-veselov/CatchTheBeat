using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalStatistics : MonoBehaviour {

	public static int missed_fruits=0;

	public static int big_fruits_counter=0;
	public static int small_fruits_counter=0;
	public static int medium_fruits_counter = 0;

	public static int comboCounter = 0;
	public static int maxCombo = 114 ;

	public static long finalScore = 0;

	public static float accuracy = 0f;

	// Use this for initialization
	void Start () {
		


	}
	
	// Update is called once per frame
	void Update () {

		if (comboCounter > maxCombo) {
		
			maxCombo = comboCounter;
		}

		getAccuracy ();
	}

	void getAccuracy() {

		if (big_fruits_counter > 0 || missed_fruits > 0) {
			int sum1 = small_fruits_counter + big_fruits_counter + medium_fruits_counter;
			int sum2 = small_fruits_counter + big_fruits_counter + medium_fruits_counter + missed_fruits;
			accuracy = (float)sum1 / (float)sum2;
			accuracy *= 100.0f;
		}

	}

	public static void ZeroStats() {

		missed_fruits = 0;

		big_fruits_counter = 0;
		small_fruits_counter = 0;
		medium_fruits_counter = 0;

		comboCounter = 0;
		maxCombo = 0 ;

		finalScore = 0;

		accuracy = 0f;


	}
}
