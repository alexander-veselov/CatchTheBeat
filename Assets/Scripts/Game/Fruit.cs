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
    public float thisFruitSpeed;
    private Score_Numbers_Instance combo_inst;

    private static Sprite[] fruitSprites;
    private Color32 color;
	byte counter = 0;
    public bool isHasted = false;
	Vector2 variableForSpriteSize;
	public bool isCatchedEarlier;
    public bool isFinishing= false;
    void Awake()
    {

        sprites = GetComponentsInChildren<SpriteRenderer>();
        combo_inst = Camera.main.GetComponent<Score_Numbers_Instance>();
        //		statistics = Camera.main.GetComponent<finalStatistics>();
    }

    private void Start()
    {
        fruitSprites = new Sprite[4];
        fruitSprites[0] = Resources.Load<Sprite>("Fruit sprites/apple");
        fruitSprites[1] = Resources.Load<Sprite>("Fruit sprites/pear");
        fruitSprites[2] = Resources.Load<Sprite>("Fruit sprites/orange");
        fruitSprites[3] = Resources.Load<Sprite>("Fruit sprites/grape");
        
        sprites[0].color = color;
        if (type == types.FRUIT)
        {
            setRandomFruit();
        }   
                thisFruitSpeed = speed;
		isCatchedEarlier = false;

		variableForSpriteSize = sprites[1].bounds.size;  
    }
    public void initialize(Color32 col, types t, bool hasted, bool isF)
    {
        isHasted = hasted;
        if (isHasted) sprites[2].color = new Color(1, 1, 1, 0.7f);
        color = col;
        type = t;
        isFinishing = isF;
    }
    void Update ()
    {
        Vector3 dir = transform.up;
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        transform.position = Vector3.MoveTowards(transform.position, transform.position - dir, thisFruitSpeed * Time.deltaTime);
        if (transform.position.y < min.y)
        {

			if (this.isCatchedEarlier == false && !pause.isPaused) {
				health.sub ();
			}
            counter++;
			if (counter == 1) {
				doDestroyFruit ();
			}

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

	private void doDestroyFruit() {

		Destroy(this.gameObject, .2f);

		if(this.isCatchedEarlier == false){
			
		finalStatistics.missed_fruits++;
			//health.sub();
		if (type == types.FRUIT) {
			combo_inst.docleanCombo ();
			ScatterOfFruits.doFallFruts ();
		}
		}
	}

void OnTriggerEnter2D (Collider2D col) {

		if (col.gameObject.name != "PlayerSprite") return;
		if (this.name == "drop(Clone)" || this.name == "Little Fruit(Clone)") { DestroyObject (this.gameObject); return; }

		if (this.gameObject.transform.position.y - (Math.Abs(sprites[1].bounds.size.y / 2.0f) - 0.6f) >= 
            col.transform.position.y + col.offset.y + (col.bounds.size.y/2.0f) &&
            (this.gameObject.transform.position.x >= 
            (GameObject.Find ("Player").transform.position.x
            - (GameObject.Find ("Player").GetComponentInChildren<SpriteRenderer> ().bounds.size.x / 2f)))
            && (this.gameObject.transform.position.x <=
            (GameObject.Find ("Player").transform.position.x
            + (GameObject.Find ("Player").GetComponentInChildren<SpriteRenderer> ().bounds.size.x / 2f)))) {

		   
	
			this.GetComponent<CircleCollider2D> ().enabled = false;
            Vector3 s = GameObject.Find("Player").GetComponentInChildren<SpriteRenderer>().transform.localScale;

            this.transform.localScale = new Vector2 (s.x / 2.4f, s.y / 2.4f);

			this.transform.position = new Vector2 (this.transform.position.x, this.transform.position.y - 0.5f);

			ScatterOfFruits.doAddFruitToPlayer (this.gameObject);

			this.isCatchedEarlier = true;

//			this.transform.localScale = new Vector2 (GameObject.Find ("Player").transform.localScale.x / 2f, GameObject.Find ("Player").transform.localScale.y / 2f);
//
//			this.transform.position = new Vector2 (this.transform.position.x, this.transform.position.y + (sprites[1].size.y/2f - variableForSpriteSize.y/2f - 0.5f));

		} else {
		
			DestroyObject (this.gameObject);
		
		}
	
	
	
	}
}
