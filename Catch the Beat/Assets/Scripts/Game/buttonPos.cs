using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonPos : MonoBehaviour
{

    [SerializeField]
    private float dir;

    private Image sprite;

    void Start()
    {  
        MapsLoad.bg = GameObject.Find("background").GetComponent<Canvas>();
        
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        Vector2 sc = max;
        sc.y /= 6f;
        sc.x = sc.y;
        
        float dx = (max.x - min.x) * 0.07f*1.3f;
        float dy = (max.y - min.y) * 0.16f;
        sprite = GetComponentInChildren<Image>();
        sprite.color = new Color(1, 1, 1, 0.5f);
        sprite.transform.localScale = new Vector3(Screen.height / 300f   , Screen.height / 300f, 1);
        if (dir == 0)
        {
            sprite.transform.position = new Vector3(min.x + dx, min.y/6f);
        }
        if (dir == 1)
        {
            sprite.transform.position = new Vector3(max.x - dx, min.y/6f);
        }
        if (dir == 2)
        {
            sprite.transform.position = new Vector3(min.x + dx, min.y + dy);
        }
        if (dir == 3)
        {
            sprite.transform.position = new Vector3(max.x - dx, min.y + dy);
        }
        if (dir == 4)
        {
            sprite.transform.position = new Vector3(min.x + dx, -min.y/1.5f -dy);
        }
        if (dir == 5)
        {
            sprite.transform.position = new Vector3(min.x + 8.5f*dx, min.y + dy);
        }
    }

  

    void Update()
    {

    }
}
