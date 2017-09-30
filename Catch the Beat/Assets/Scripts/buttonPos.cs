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
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        float dx = (max.x - min.x) * 0.07f;
        float dy = (max.y - min.y) * 0.16f;
        sprite = GetComponentInChildren<Image>();
        if (dir == 0)
        {
            sprite.transform.position = new Vector3(min.x + dx, min.y + dy + 1.3f * dy);
        }
        if (dir == 1)
        {
            sprite.transform.position = new Vector3(max.x - dx, min.y + dy + 1.3f * dy);
        }
        if (dir == 2)
        {
            sprite.transform.position = new Vector3(min.x + dx, min.y + dy);
        }
        if (dir == 3)
        {
            sprite.transform.position = new Vector3(max.x - dx, min.y + dy);
        }
    }

    void Update()
    {

    }
}
