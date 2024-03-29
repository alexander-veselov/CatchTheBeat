﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class modButton : MonoBehaviour {
    enum modType
    {
        NF, DT, HD, AD
    }
    [SerializeField]
    modType type;
    Vector2 normalScale;
    Vector2 incScale;
    Color color;
    bool isActive=false;

    void Start () {
        GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        if (type == modType.AD && MapsLoad.AD == true)
        {
            upAnimation();
            isActive = true;
        }
        if (type == modType.DT && MapsLoad.DT == true)
        {
            Time.timeScale = 1.4f;
            AudioLoad.audioSource.pitch = 1.4f;
            upAnimation();
            isActive = true;
        }
        if (type == modType.NF && MapsLoad.NF == true)
        {
            upAnimation();
            isActive = true;
        }
        if (type == modType.HD && MapsLoad.HD == true)
        {
            upAnimation();
            isActive = true;
        }
        normalScale = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        normalScale.y = Screen.height/1100f;
        normalScale.x = normalScale.y;
        transform.localScale = normalScale;
        Vector3 max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1,1));
        max.x = Screen.width*0.05f + (int)type* Screen.width *0.07f;
        max.y = Screen.height / 11f;
        transform.position = max;
        incScale = normalScale * 1.1f;
    }
	
    public void change()
    {
        GameObject.Find("sounds").GetComponent<sounds>().ModClap();
        if (isActive)
        {
            isActive = false;
            downAnimation();
            apply();
        }
        else
        {
            isActive = true;
            upAnimation();
            apply();
        }
    }

    void upAnimation()
    {
        GetComponent<Image>().color = new Color(1, 1, 1, 1);
        transform.localScale = incScale;
        transform.Rotate(0, 0, -10);        
    }

    void downAnimation()
    {
        transform.localScale = normalScale;
        transform.Rotate(0, 0, 10);
        GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
    }

    void apply()
    {
        if (type == modType.AD)
        {
            MapsLoad.AD = !MapsLoad.AD;
        }
        if (type == modType.DT)
        {
            MapsLoad.DT = !MapsLoad.DT;
            if (MapsLoad.DT)
            {
                AudioLoad.audioSource.pitch = 1.4f;
                Time.timeScale = 1.4f;
            }
            else
            {
                AudioLoad.audioSource.pitch = 1;
                Time.timeScale = 1;
            }
            
        }
        if (type == modType.HD)
        {
            MapsLoad.HD = !MapsLoad.HD;
        }
        if (type == modType.NF)
        {
            MapsLoad.NF = !MapsLoad.NF;
        }
    }
	void Update () {
		
	}
}
