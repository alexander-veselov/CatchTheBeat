using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class logo : MonoBehaviour {

    [SerializeField]
    MapsLoad mapLoad;
    SpriteRenderer[] sprites;
    beatEffect bEff;
    Image[] lines;
    AudioClip myAudioClip;
    AudioSource audioSource;
    ArrayList array;
    float timeBegin;
  
    private StreamReader input;
    void Start () {
        sprites = GetComponentsInChildren<SpriteRenderer>();
        beatEffect bEff = GetComponentInChildren<beatEffect>();
        sprites[0].transform.localScale = new Vector3(Screen.height * 0.07f, Screen.height * 0.07f, 1);
        sprites[0].transform.position = new Vector2(0, 0);
        sprites[1].transform.localScale = new Vector3(Screen.height * 0.07f, Screen.height * 0.07f, 1);
        sprites[1].transform.position = new Vector2(0, 0);
        sprites[1].color = new Color(1, 1, 1, 0.15f);
        lines = GetComponentsInChildren<Image>();
        lines[1].transform.localScale = new Vector3(1, Screen.height * 0.001f, 1);
        lines[2].transform.localScale = new Vector3(1, Screen.height * 0.001f, 1);
    }
    public void beat()
    {
        GetComponentInChildren<beatEffect>().beat(sprites[1].transform.localScale);
    }
    public void goToMenu()
    {
        SceneManager.LoadScene("menu");
        mapLoad.loadType = 2;
    }
}
