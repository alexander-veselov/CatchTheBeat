using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour {

    Vector3 scale = Vector3.zero;
    Rigidbody2D rg;
    bool isParent;
    Color color;
    // GameObject gameObj;
    SpriteRenderer sprite;
    float sp = 0.2f;
    float dx;
    void Start()
    {

    }


    public void ini(Effects gameObj, Color32 col, float dX, bool isP)
    {
        dx = dX;
        isParent = isP;
        
        rg = gameObj.GetComponentInChildren<Rigidbody2D>();
        sprite = gameObj.GetComponentInChildren<SpriteRenderer>();
        sprite.color = col;
        color = col;


    }
    int j = 0;
	void Update () {


            color = new Color(color.r, color.g, color.b, color.a-0.035f);
        if (color.a <= 0) Destroy(this.gameObject);
        sprite.color = color;
    }
}
