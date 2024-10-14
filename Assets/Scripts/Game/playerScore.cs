using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScore : MonoBehaviour {

    SpriteRenderer[] bits;
    public long score=0;
    public int type = 0;
    static Sprite[] sprites;
    int multiplier = 2;
    void Start ()
    {
        if (type == 0)
        {
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(0.98f, 0.95f));
            transform.position = max;
        }
        if (type == 1)
        {
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(0.28f, 0.25f));
            transform.position = max;
        }

        sprites = new Sprite[10];
            for (int i = 0; i < 10; i++)
            {
                sprites[i] = Resources.Load<Sprite>("Numbers/" + i.ToString());
               
            }
            bits = new SpriteRenderer[8];
            bits = GetComponentsInChildren<SpriteRenderer>();
        if (type == 1)
            for (int i = 0; i < 8; i++) bits[i].color = new Color(1, 1, 1, 0.85f);
            if (MapsLoad.HD) multiplier += 2;
            if (MapsLoad.DT) multiplier += 1;
            if (MapsLoad.NF) multiplier -= 1;
        
    }

    public void scoreUp()
    {
        score += (int)Player.combo * multiplier;
        updateScore();
    }
	
    public void updateScore()
    {
        for(int j=0; j<8; j++) bits[j].sprite = sprites[0];
        long sc = score;
        int i = 0;
        while(sc > 0)
        {
            bits[i++].sprite = sprites[sc % 10];
            sc /= 10;
        }
    }
    public void setScore()
    {
        score = records.getRecord(MenuLoad.map);

        updateScore();
    }

    void Update ()
    {
		
	}
}
