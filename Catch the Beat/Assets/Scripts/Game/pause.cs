using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pause : MonoBehaviour {

    Image[] buttons;
    bool isPaused=false;
	void Start () {
        GameObject.Find("pauseBG").GetComponent<SpriteRenderer>().enabled = false;
        buttons = GameObject.Find("Pause").GetComponentsInChildren<Image>();
        foreach(Image i in buttons)
        {
            i.enabled = false;
            i.transform.localScale = new Vector3(1f, Screen.height/400f,1f);
        }

        buttons[1].transform.position = new Vector3(Screen.width/2, Screen.height / 4, 1);
        buttons[2].transform.position = new Vector3(Screen.width / 2, Screen.height / 2.1f, 1);
        buttons[0].transform.position = new Vector3(Screen.width / 2, Screen.height / 4*2.85f, 1);
        bgLoad();
	}
	
	void Update () {
        if (!isPaused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameObject.Find("sounds").GetComponent<sounds>().Pause();
                GameObject.Find("pauseBG").GetComponent<SpriteRenderer>().enabled= true;
                foreach (Image i in buttons)
                {
                    i.enabled = true;
                }
                isPaused = true;
                Time.timeScale = 0;
                AudioLoad.audioSource.Pause();
            }
        } else
        if (isPaused)
        {
            //GameObject.Find("sounds").GetComponent<sounds>().PauseOff();
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameObject.Find("sounds").GetComponent<sounds>().MenuClick();
                AudioLoad.audioSource.Play();
                GameObject.Find("pauseBG").GetComponent<SpriteRenderer>().enabled = false;
                pauseOff();
            }
        }
       
    }
    void bgLoad()
    {
        string s = MapsLoad.currentMap;
        SpriteRenderer image = GameObject.Find("pauseBG").GetComponent<SpriteRenderer>();
        
        image.sprite = Resources.Load<Sprite>("pauseBG");
        var width = image.sprite.bounds.size.x;
        var height = image.sprite.bounds.size.y;
        var worldScreenHeight = Camera.main.orthographicSize * 2.0;
        var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
        image.transform.localScale = new Vector2((float)worldScreenWidth / width, (float)worldScreenHeight / height);
    }
    void pauseOff()
    {
        
        foreach (Image i in buttons)
        {
            i.enabled = false;
        }
        isPaused = false;
        if (MapsLoad.DT)
        {
            Time.timeScale = 1.4f;
        }
        else
        {
            Time.timeScale = 1;
        }
        
    }
    public void Quit()
    {
        GameObject.Find("sounds").GetComponent<sounds>().MenuClick();
        pauseOff();
        AudioLoad.audioSource.Play();
        GameObject.Find("mapScript").GetComponent<MapsLoad>().loadType = 2;
        SceneManager.LoadScene("menu");
    }
    public void Continue()
    {
        GameObject.Find("sounds").GetComponent<sounds>().MenuClick();
        GameObject.Find("pauseBG").GetComponent<SpriteRenderer>().enabled = false;
        AudioLoad.audioSource.Play();
        pauseOff();
    }
    public void Retry()
    {
        GameObject.Find("sounds").GetComponent<sounds>().MenuClick();
        health.restart();
        pauseOff();
        AudioLoad.audioSource.time=0;
        GameObject.Find("mapScript").GetComponent<MapsLoad>().fileParse();
        GameObject.Find("mapScript").GetComponent<MapsLoad>().bitLoad();
        SceneManager.LoadScene("scene");
    }
}
