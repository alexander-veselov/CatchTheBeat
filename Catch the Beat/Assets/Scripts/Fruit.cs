using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class Fruit : MonoBehaviour {

    public enum types
    {
        FRUIT, DROPx2, DROP
    }
    
    [SerializeField]
    public types type = types.FRUIT;
    private SpriteRenderer[]  sprites;
    public static float speed = 15;
    private Score_Numbers_Instance inst;
    private static Sprite[] fruitSprites;
    private Color32 color;
    void Awake()
    {


        inst = Camera.main.GetComponent<Score_Numbers_Instance>();

    }

    private void Start()
    {
        fruitSprites = new Sprite[4];
        fruitSprites[0] = Resources.Load<Sprite>("Fruit sprites/apple");
        fruitSprites[1] = Resources.Load<Sprite>("Fruit sprites/pear");
        fruitSprites[2] = Resources.Load<Sprite>("Fruit sprites/orange");
        fruitSprites[3] = Resources.Load<Sprite>("Fruit sprites/grape");
        sprites = GetComponentsInChildren<SpriteRenderer>();
        sprites[0].color = color;
        if (type == types.FRUIT)
        {
            setRandomFruit();
        }     
    }
    public void initialize(Color32 col, types t)
    {
        color = col;
        type = t;
    }
    void Update ()
    {
        Vector3 dir = transform.up;
        transform.position = Vector3.MoveTowards(transform.position, transform.position - dir, speed * Time.deltaTime);
        if (transform.position.y < -10)
        {
            Destroy(this.gameObject, .2f);
            inst.docleanCombo();
        }
    }
	private void setRandomFruit()
    {
        int typeNum = UnityEngine.Random.Range(1, 4);
        switch (typeNum)
        {
            case 1:
                sprites[0].sprite = fruitSprites[0];
                break;
            case 2:
                sprites[0].sprite = fruitSprites[1];
                break;
            case 3:
                sprites[0].sprite = fruitSprites[2];
                break;
            case 4:
                sprites[0].sprite = fruitSprites[3];
                break;
        }
        
    }

}
