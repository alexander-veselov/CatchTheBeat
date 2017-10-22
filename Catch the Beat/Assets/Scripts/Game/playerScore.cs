using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScore : MonoBehaviour {

    SpriteRenderer[] bits;
    public long score=0;
    static Sprite[] sprites;
    int multiplier = 2;
    void Start ()
    {
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(0.98f, 0.95f));
        transform.position = max;
        sprites = new Sprite[10];
        for(int i=0; i<10; i++)
        {
            sprites[i] = Resources.Load<Sprite>("Numbers/" + i.ToString());
        }
        bits = new SpriteRenderer[8];
        bits = GetComponentsInChildren<SpriteRenderer>();
        if (MapsLoad.HD) multiplier += 2;
        if (MapsLoad.DT) multiplier += 1;
        if (MapsLoad.NF) multiplier -= 1;
        //if (MapsLoad.AD) multiplier = 0;
    }

    public void scoreUp()
    {
        score += (int)Player.combo * multiplier;
        setScore();
    }
	
    public void setScore()
    {
        long sc = score;
        int i = 0;
        while(sc > 0)
        {
            bits[i++].sprite = sprites[sc % 10];
            sc /= 10;
        }
    }
	void Update ()
    {
		
	}
}
