using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class Fruit : MonoBehaviour {

    [SerializeField]
    public static float speed = 15;
    private static SpriteRenderer[]  sprites;

    private Color32 purple;
    private Color32 red;
    private Color32 green;
    private Color32 blue;

 

    private void Start()
    {
        purple = new Color32 (158, 47, 255, 255);
        red = new Color32 (255, 76, 185, 255);
        green = new Color32(36, 166, 101, 255);
        blue = new Color32 (46, 132, 164, 255);


        sprites = GetComponentsInChildren<SpriteRenderer>();
        float rand = 0.0f;

        //rand = UnityEngine.Random.Range (-100.0f,101.0f);

        //_sprite.transform.Rotate (Vector3.forward * rand);
        setRandomColor ();

    }

    void Update ()
    {
        Vector3 dir = transform.up;
        transform.position = Vector3.MoveTowards(transform.position, transform.position - dir, speed * Time.deltaTime);
        if (transform.position.y < -10)
        {
            Destroy(this.gameObject, .2f);
        }


        
    }
		
	private void setRandomColor() {
		
		int rand;

		rand = UnityEngine.Random.Range (1,5);

		switch (rand) {

		case 1:
			sprites[0].color = purple;     //purple
         	break;
		case 2:
			sprites[0].color = blue ;     //blue
			break;
		case 3:
			sprites[0].color = green;     //green
			break;
		case 4:
			sprites[0].color = red;      //red
			break;

		}
	
	}

}
