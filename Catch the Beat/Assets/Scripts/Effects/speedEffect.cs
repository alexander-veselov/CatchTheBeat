using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedEffect : MonoBehaviour {

    
    SpriteRenderer sprite;
    Color color;
	void Start () {
        
        
        
	}



    public void setTransparency(SpriteRenderer sr, float alpha)
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.flipX = sr.flipX;
       // sprite.sprite = sr.sprite;
        sprite.transform.localScale = MapsLoad.scale*1.25f;
        color = new Color(1, 1, 1, alpha);
    }
	void Update () {
        if (sprite.color.a <= 0) Destroy(this.gameObject);
        color.a -= 0.1f;
        sprite.color = color;
        
	}
}
