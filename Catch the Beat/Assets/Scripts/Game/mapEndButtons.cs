using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mapEndButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public void backToMenu()
    {
        SceneManager.LoadScene("menu");
        GameObject.Find("bgMusic").GetComponent<MapsLoad>().loadType = 2;
    }
	void Update () {
		
	}
}
