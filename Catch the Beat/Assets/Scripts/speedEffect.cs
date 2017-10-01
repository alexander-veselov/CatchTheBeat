using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedEffect : MonoBehaviour {

    
    SpriteRenderer sprite;
    Color color;
	void Start () {
        
        
        
	}

    void spriteLight()
    {
        Texture2D tex = sprite.sprite.texture;
        Texture2D newTex = (Texture2D)GameObject.Instantiate(tex);
        newTex.SetPixels32(tex.GetPixels32());
        for (int i = 0; i < newTex.width; i++)
        {
            for (int j = 0; j < newTex.height; j++)
            {
                if (newTex.GetPixel(i, j).a != 0f) newTex.SetPixel(i, j, newTex.GetPixel(i, j) * 1.5f);

            }
        }

        newTex.Apply();
        sprite.sprite = Sprite.Create(newTex, sprite.sprite.rect, new Vector2(0.5f, 0.5f));
    }

    public void setTransparency(SpriteRenderer sr, float alpha)
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.flipX = sr.flipX;
       // sprite.sprite = sr.sprite;
        sprite.transform.localScale = MapsLoad.scale;
        color = new Color(1, 1, 1, alpha);
    }
	void Update () {
        if (sprite.color.a <= 0) Destroy(this.gameObject);
        color.a -= 0.1f;
        sprite.color = color;
        
	}
}
