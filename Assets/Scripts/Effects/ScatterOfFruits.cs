using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScatterOfFruits : MonoBehaviour {
	public static GameObject fruitPref;

	bool doScatter;

	static public Vector2 playerPos;

	static public SpriteRenderer playerSprite;

	public static List<GameObject> l;

	public static ArrayList arrOfFruits;

	public static List<Vector2> fruitPos;

	public static Vector3 p;
    public static float h = 0;
	// Use this for initialization
	void Start () {

		fruitPos = new List<Vector2>();
		
		p = Camera.main.WorldToViewportPoint (new Vector2(0,1));

		fruitPref = Resources.Load<GameObject> ("Prefabs/fruitForScatter");

//		fruitPref.transform.localScale = new Vector2 (p.y/1.25f ,p.y/1.25f);
	

		l = new List<GameObject> ();


		playerSprite = GameObject.Find ("Player").GetComponentInChildren<SpriteRenderer> ();


//		get_position ();

	}
	
	// Update is called once per frame
	void Update () {

		playerPos = GameObject.Find ("Player").transform.position;


			if (l != null) {	

				for (int i = 0; i < l.Count; i++) {
                    if (l[i]!=null)
					l [i].transform.position = new Vector2 (fruitPos [i].x + playerPos.x, fruitPos [i].y);

				}
			}
		

		if (Player.isFinishing || l.Count >=15) {
            //			doScatter = true;
            Player.isFinishing = false;

            doScatterAnimation ();

//			foreach(GameObject gm in l){
//
//				Destroy (gm,5f);
//			}

			l.Clear ();
			fruitPos.Clear ();




		}


	}

	public static void doAddFruitToPlayer(GameObject gm) {


		gm.GetComponent<Fruit> ().thisFruitSpeed = 0;

        //		if (gm.GetComponent<Fruit> ().type == Fruit.types.FRUIT) {
        //		
        //		
        //			gm.transform.localScale = new Vector2 (gm.transform.localScale.x / 2f, gm.transform.localScale.y / 2f);
        //
        //		}
        
		fruitPos.Add (new Vector2((UnityEngine.Random.Range(0f, 0.6f)*playerPos.x)*0.8f,gm.transform.position.y));
	
		l.Add (gm);
	
	}
		


	public void doScatterAnimation() {
        h = 0;

        foreach (GameObject gm in l)  {



            gm.GetComponent<Rigidbody2D> ().gravityScale = 1.5f;

			gm.GetComponent<Rigidbody2D> ().AddForce(new Vector2(UnityEngine.Random.Range(-100f,180f),UnityEngine.Random.Range(120,280f)));

		}



	}

	public static void doFallFruts() {
        h = 0;
		foreach(GameObject gm in l){

			gm.GetComponent<Rigidbody2D> ().gravityScale = 0.85f;

			gm.GetComponent<Rigidbody2D> ().AddForce(new Vector2(UnityEngine.Random.Range(-50f,51f),0));

		}

		l.Clear ();
		fruitPos.Clear ();

	}

	void doDecreaseSizeOfFruit() {


	}
}
