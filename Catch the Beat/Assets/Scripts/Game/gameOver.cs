using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameOver : MonoBehaviour {

    Image[] buttons;
    SpriteRenderer image;
    bool isOver = false;
    void Start ()
    {
        GameObject.Find("endBG").GetComponent<SpriteRenderer>().enabled = false;
        buttons = GameObject.Find("gameOver").GetComponentsInChildren<Image>();
        foreach (Image i in buttons)
        {
            i.enabled = false;
            i.transform.localScale = new Vector3(1f, Screen.height / 400f, 1f);
        }

        buttons[1].transform.position = new Vector3(Screen.width / 2, Screen.height / 4, 1);
        buttons[0].transform.position = new Vector3(Screen.width / 2, Screen.height / 2.1f, 1);
        bgLoad();
    }
    void bgLoad()
    {
        string s = MapsLoad.currentMap;
        image = GameObject.Find("endBG").GetComponent<SpriteRenderer>();

        image.sprite = Resources.Load<Sprite>("endBG");
        var width = image.sprite.bounds.size.x;
        var height = image.sprite.bounds.size.y;
        var worldScreenHeight = Camera.main.orthographicSize * 2.0;
        var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
        image.transform.localScale = new Vector2((float)worldScreenWidth / width, (float)worldScreenHeight / height);
        image.color = new Color(1, 1, 1, 0.7f);
    }
    public void endGame()
    {
    
        isOver = true;
        GameObject.Find("endBG").GetComponent<SpriteRenderer>().enabled = true;
        foreach (Image i in buttons)
        {
            i.enabled = true;
        }
        AudioLoad.audioSource.Pause();

    }
    void pauseOff()
    {

        foreach (Image i in buttons)
        {
            i.enabled = false;
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
    public void Retry()
    {
        GameObject.Find("sounds").GetComponent<sounds>().MenuClick();
        health.restart();
        pauseOff();
        AudioLoad.audioSource.time = 0;
        SceneManager.LoadScene("scene");
    }
    void Update () {
        if (isOver) image.color = new Color(1, 1, 1, image.color.a+0.005f);
    }
}
