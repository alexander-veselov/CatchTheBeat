﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mapEndButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public void backToMenu()
    {
        GameObject.Find("sounds").GetComponent<sounds>().MapEndOff();
        GameObject.Find("sounds").GetComponent<sounds>().MenuClick();
        GameObject.Find("mapScript").GetComponent<MapsLoad>().loadType = 2;
        SceneManager.LoadScene("menu");
    }
	void Update () {
		
	}
}
