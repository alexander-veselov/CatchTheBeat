using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class MapsLoad : MonoBehaviour {
	

	Fruit apple;
	Fruit banana;
	Fruit grape;
	Fruit orange;
	Fruit pear;

    public class fruit_point
    {
        public fruit_point(int X, int Y, int Time)
        {
            x = X;
            y = Y;
            time = Time;
        }
        public int x;
        public int y;
        public int time;
    }

    private AudioSource audioSource;
    private int count = 0;
    private ArrayList array;
    private StreamReader input;

    public static float HPDrainRate;
    public static float CircleSize;
    public static float OverallDifficulty;
    public static float ApproachRate;

    private float lenX, lenY, maxY;
    public static Vector3 scale = new Vector3(1,1,1);

    private void Start()
    {
		transform.rotation = new Quaternion (0, 0, 0, transform.rotation.w);

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        lenX = max.x - min.x - 3f*Player.sprite.size.x;
        lenX = max.y - min.y;
        maxY = max.y;
        apple = Resources.Load<Fruit> ("Fruit_apple");
		grape = Resources.Load<Fruit> ("Fruit_grape");
		orange = Resources.Load<Fruit> ("Fruit_orange");
		pear = Resources.Load<Fruit> ("Fruit_pear");
        

        input = File.OpenText(Application.persistentDataPath+ '/' + MenuLoad.folder + '/' + MenuLoad.map);
        String  str;
        while ((str = input.ReadLine()) != null)
        {
            if (str == "[Difficulty]") break;
        }
        str = input.ReadLine();
        HPDrainRate = float.Parse(str.Substring(12));
        str = input.ReadLine();
        CircleSize = float.Parse(str.Substring(11));
        str = input.ReadLine();
        HPDrainRate = float.Parse(str.Substring(18));
        str = input.ReadLine();
        ApproachRate = float.Parse(str.Substring(13));
        while ((str = input.ReadLine()) != null)
		{
            if (str == "[HitObjects]") break;
        }
        array = new ArrayList();
        while ((str = input.ReadLine()) != null)
		{
            string[] a = str.Split(',');
            array.Add(new fruit_point(int.Parse(a[0]), int.Parse(a[1]), int.Parse(a[2])));
        }
        scale = new Vector3(0.5f+1/CircleSize, 0.5f+1/CircleSize, 1);
        Fruit.speed = ApproachRate*max.y/2.6f;
        apple.transform.localScale = scale;
        grape.transform.localScale = scale;
        orange.transform.localScale = scale;
        pear.transform.localScale = scale;
    }

    private bool isPlaying= false;
    void Update () {
		
        foreach (fruit_point f in array)
        {
            if (f.time<= ((Time.time - MenuLoad.timeBegin) * 1000))
            {

                if (!isPlaying)
                {
                    AudioLoad.audioSource.Play();
                    isPlaying = true;
                }



               


                
                Vector3 pos = Vector3.zero;
                pos.x = f.x * lenX / 512f - lenX / 2f ;
                pos.y = maxY;





				switch (UnityEngine.Random.Range (1, 5)) {

				case 1:
					
					Instantiate (apple, pos, transform.rotation);
					break;
				case 2:
					
					Instantiate (grape, pos, transform.rotation);
					break;
				case 3:
					
					Instantiate (orange, pos, transform.rotation);
					break;
				case 4:
					
					Instantiate (pear, pos, transform.rotation);
					break;

				}


									
                array.RemoveAt(0);
                break;
                
            }
        }
    }
}
