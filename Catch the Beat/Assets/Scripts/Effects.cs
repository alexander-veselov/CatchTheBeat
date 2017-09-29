using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour {

    Vector3 scale = Vector3.zero;
    Rigidbody2D rg;
    int _type;
    Color color;
    // GameObject gameObj;
    SpriteRenderer sprite;
    float sp = 0.2f;
    float dx;
    void Start()
    {

    }


    public void ini(Effects gameObj, Color32 col, float dX, int type)
    {
        dx = dX;
        _type = type;
        
        rg = gameObj.GetComponentInChildren<Rigidbody2D>();
        sprite = gameObj.GetComponentInChildren<SpriteRenderer>();
        sprite.transform.localScale = MapsLoad.scale;
        sprite.color = col;
        color = col;


    }
    int j = 0;
	void Update () {
        Vector2 pos = Player.sprite.transform.position;
        pos.x += dx;
        pos.y = transform.position.y;
       
        transform.position = pos;
        if (_type==0) color = new Color(color.r, color.g, color.b, color.a-0.035f);
        if (_type == 1) color = new Color(color.r, color.g, color.b, color.a - 0.025f);
        if (color.a <= 0) Destroy(this.gameObject);
        sprite.color = color;
    }
}
