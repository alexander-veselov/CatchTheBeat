using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour {

    SpriteRenderer _sprite;
    public static float HP = 1;
    bool failed = false;
    Vector2 scale;
    float fullX;
    gameOver go;
	void Start ()
    {
        failed = false;
        scale = Camera.main.ViewportToWorldPoint(new Vector2(0, 1));
        go = GameObject.Find("gameOver").GetComponent<gameOver>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
        scale.y *= 0.95f;
        scale.x *=1.05f;
        _sprite.transform.position = scale;
        scale = Camera.main.ViewportToWorldPoint(new Vector2(0, 1));
        scale.y = 1f;
        scale.x /=-12.5f;
        _sprite.transform.localScale = scale;
        fullX = scale.x;
        if (MapsLoad.NF == true)  _sprite.color = new Color(1, 1, 1, 0);

    }
	public static void restart()
    {
        HP = 1;
    }
    public static  void add()
    {
        if (HP < 1) HP += 0.01f * MapsLoad.ApproachRate/10f;
    }
    public static void sub()
    {
        HP -= 0.003f*MapsLoad.ApproachRate/10f;
    }
    void Update ()
    {
        if (AudioLoad.audioSource.isPlaying) HP -= 0.0004f * MapsLoad.ApproachRate / 10f;
        if (HP <= 0.03f && MapsLoad.NF == false && failed == false)
        {
            GameObject.Find("sounds").GetComponent<sounds>().Failsound();
            failed = true;
            go.endGame();

        }
        scale.x = HP*fullX;
        _sprite.transform.localScale = scale;
    }
}
