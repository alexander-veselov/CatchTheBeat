using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
	
    [SerializeField]
    private float speed;

    private bool isMovingRight = false;
    private bool isMovingLeft = false;
    private bool isHasted = false;
    public static playerScore score;
    private float dt;
    private Score_Numbers_Instance combo_inst;
//	private finalStatistics statistics;
    public static SpriteRenderer sprite;
    public Sprite s;
    public static speedEffect seff;
    public static BoxCollider2D _collider;
    int useCount=0;
	public static int comboEff;

    private void Awake()

    {

		combo_inst = Camera.main.GetComponent<Score_Numbers_Instance>();
//		statistics = Camera.main.GetComponent<finalStatistics> ();
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        speed = (max.x - min.x)/1.6f;
        sprite = GetComponentInChildren<SpriteRenderer>();
        _collider = GetComponentInChildren<BoxCollider2D>();
        seff = Resources.Load<speedEffect>("Prefabs/speedEffect");
		combo_inst.fruit_counter = 0;
        
    }

    private void Update()
    {
        speedEffect();
        sprite.transform.localScale = MapsLoad.scale*1.25f;
        Move();
        if (Input.GetKeyDown(KeyCode.LeftArrow)) moveLeft();
        if (Input.GetKeyUp(KeyCode.LeftArrow)) stopLeft();
        if (Input.GetKeyDown(KeyCode.RightArrow)) moveRight();
        if (Input.GetKeyUp(KeyCode.RightArrow)) stopRight();
        if (Input.GetKeyDown(KeyCode.LeftShift)) startHaste();
        if (Input.GetKeyUp(KeyCode.LeftShift)) stopHaste();
    }
    void OnTriggerEnter2D (Collider2D col)
    {
        
		comboEff = (int)combo_inst.fruit_counter;
		finalStatistics.comboCounter = (int)combo_inst.fruit_counter;
		finalStatistics.finalScore = score.score;
       
        score.scoreUp();
        Fruit f = col.GetComponent<Fruit>();
        Vector2 pos = transform.position;
        Effects eff;
		if (f.type == Fruit.types.FRUIT)
        {
			combo_inst.fruit_counter++;
			combo_inst.Boom();
			finalStatistics.big_fruits_counter++;
            pos.y = transform.position.y + sprite.size.y * MapsLoad.scale.y/1.52f ;
        eff = Instantiate(Resources.Load<Effects>("Prefabs/HitEffect"), pos, transform.rotation);
        eff.initialize(eff, col.gameObject.GetComponentInChildren<SpriteRenderer>().color, (col.gameObject.transform.position.x - sprite.transform.position.x)*0.45f,0);
        }

		if (f.type == Fruit.types.DROPx2) {
			
			finalStatistics.medium_fruits_counter++;

		}

		if (f.type == Fruit.types.DROP) {
		
			finalStatistics.small_fruits_counter++;

		}

        pos.y = transform.position.y + sprite.size.y * MapsLoad.scale.y / 1.52f;
         eff = Instantiate(Resources.Load<Effects>("Prefabs/HitEffect 1"), pos, transform.rotation);
        eff.initialize(eff, col.gameObject.GetComponentInChildren<SpriteRenderer>().color, (col.gameObject.transform.position.x - sprite.transform.position.x) * 0.45f, 1);
        Destroy(col.gameObject);
    }
    private void Move()
    {

		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0,0));
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2(1,1));

		max.x = max.x - (1.5f*sprite.size.x);

		min.x = min.x + (1.5f*sprite.size.x);


        if (isMovingLeft)
        {
			Vector3 position = transform.position;

            Vector3 dir = transform.right * (-1);

			position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

			position.x = Mathf.Clamp (position.x, min.x, max.x);
			position.y =  Mathf.Clamp (position.y, min.y, max.y);

			transform.position = position;
        }
        if (isMovingRight)
        {
			Vector3 position = transform.position;

			Vector3 dir = transform.right * 1;

			position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

			position.x = Mathf.Clamp (position.x, min.x, max.x);
			position.y =  Mathf.Clamp (position.y, min.y, max.y);

			transform.position = position;
        }
        
    }

    public void moveLeft()
    {
		sprite.flipX = false;
        isMovingLeft = true;
    }

    public void stopLeft()
    {
        isMovingLeft = false;
    }

    public void moveRight()
    {
		sprite.flipX = true;
        isMovingRight = true;
    }

    public void stopRight()
    {
        isMovingRight = false;
    }

    void speedEffect()
    {
        
        if (isHasted && useCount>0)
        {
            sprite.color = new Color(1, 1, 1, sprite.color.a-0.03f);
            speedEffect eff = Instantiate(seff, transform.position, transform.rotation);
            eff.setTransparency(sprite, 0.8f);
            
        } else sprite.color = new Color(1, 1, 1, sprite.color.a + 0.03f);
    }
    public void startHaste()
    {
        useCount++;
        dt = Time.time;
        sprite.color = new Color(1, 1, 1, 0.5f);
        speed *= 2;
        //spriteLight();
        isHasted = true;
    }

    public void stopHaste()
    {
        useCount--;
        speed *= 0.5f;
        sprite.color = new Color(1, 1, 1, 1f);
        //spriteDark();
        isHasted = false;
    }


}
