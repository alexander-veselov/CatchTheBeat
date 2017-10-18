using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour {

    private Rigidbody2D rg;
    private int _type;
    private Color color;
    private SpriteRenderer sprite;
    private float dx;
    Vector3 full;
    void Start() { }

    public void initialize(Effects gameObj, Color32 col, float dX, int type)
    {
        dx = dX;
        _type = type;
        rg = gameObj.GetComponentInChildren<Rigidbody2D>();
        sprite = gameObj.GetComponentInChildren<SpriteRenderer>();
        sprite.transform.localScale = MapsLoad.scale;
        sprite.color = col;
        color = col;
        full = transform.localScale;
    }

	void Update () {
        Vector2 pos = Player.sprite.transform.position;
        pos.x += dx;
        pos.y = transform.position.y;

        transform.position = pos;
        if (_type == 0)
        {
            
            color = new Color(color.r, color.g, color.b, color.a - 0.035f);
            Vector3 sc = transform.localScale;
			int h = Player.comboEff;
            if (h > 75) h = 75;
            sc.y=(h+40)* full.y/70f;
            sc.x = (h + 175) * full.x / 200f;
            transform.localScale= sc;
        }
        if (_type == 1)
        {
            color = new Color(color.r, color.g, color.b, color.a - 0.025f);
        }
        if (color.a <= 0) Destroy(this.gameObject);
        sprite.color = color;
    }
}
