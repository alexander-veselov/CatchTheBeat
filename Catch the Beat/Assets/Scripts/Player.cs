using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	
    [SerializeField]
    private float speed = 15;

    private bool isMovingRight = false;
    private bool isMovingLeft = false;
    private bool isHasted = false;
    private long score = 0;
    private int combo = 0;

    public static SpriteRenderer sprite;
    public static BoxCollider2D _collider;

    private void Awake()

    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        _collider = GetComponentInChildren<BoxCollider2D>();
    }

    private void Update()
    {
        sprite.transform.localScale = MapsLoad.scale;
        Move();
    
    }
    void OnTriggerEnter2D (Collider2D col)
    {
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


        if (isHasted)
        {
            speed = 30;
        }
        else
        {
            speed = 15;
        }
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

    public void startHaste()
    {
        isHasted = true;
    }

    public void stopHaste()
    {
        isHasted = false;
    }


}
