using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	
    [SerializeField]
    private float speed;
	private Score_Numbers_Instance inst;
    private bool isMovingRight = false;
    private bool isMovingLeft = false;
    private bool isHasted = false;
    public static playerScore score;
    private int combo = 0;
    private float dt;

    public static SpriteRenderer sprite;
    public Sprite s;
    public static speedEffect seff;
    public static BoxCollider2D _collider;

    private void Awake()

    {
		inst = Camera.main.GetComponent<Score_Numbers_Instance> ();
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        speed = (max.x - min.x)/1.6f;
        sprite = GetComponentInChildren<SpriteRenderer>();
        _collider = GetComponentInChildren<BoxCollider2D>();
        seff = Resources.Load<speedEffect>("Prefabs/speedEffect");
        
    }

    private void Update()
    {
        speedEffect();
        sprite.transform.localScale = MapsLoad.scale;
        Move();
       


    }
    void OnTriggerEnter2D (Collider2D col)
    {
		

		inst.fruit_counter++;
		inst.Boom ();
        score.scoreUp();
        Fruit f = col.GetComponent<Fruit>();
        Vector2 pos = transform.position;
        Effects eff;
        if (f.type == 0)
        {
            pos.y = transform.position.y + sprite.size.y * MapsLoad.scale.y/1.25f ;
        eff = Instantiate(Resources.Load<Effects>("Prefabs/HitEffect"), pos, transform.rotation);
        eff.initialize(eff, col.gameObject.GetComponentInChildren<SpriteRenderer>().color, (col.gameObject.transform.position.x - sprite.transform.position.x)*0.45f,0);
        }

        pos.y = transform.position.y + sprite.size.y * MapsLoad.scale.y / 1.9f;
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
        if (isHasted && Time.time - dt > 0.05 )
        {
            speedEffect eff = Instantiate(seff, transform.position, transform.rotation);
            eff.setTransparency(sprite, 0.8f);
            
        }
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
    void spriteDark()
    {
        sprite.sprite = s;
    }
    public void startHaste()
    {
        dt = Time.time;
        speed *= 2;
        //spriteLight();
        isHasted = true;
    }

    public void stopHaste()
    {
        speed *= 0.5f;

        //spriteDark();
        isHasted = false;
    }


}
