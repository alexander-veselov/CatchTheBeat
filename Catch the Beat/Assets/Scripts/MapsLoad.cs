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
    Fruit drop;
    public class fruit_point
    {
        public fruit_point(int X, int Y, int Time, int ty)
        {
            type = ty;
            x = X;
            y = Y;
            time = Time;
        }

        public int type;
        public int x;
        public int y;
        public int time;
    }
    void parse(string str, ref ArrayList array)
    {
        
        int x1, x2, y1, y2, time, repeat, length;
        string[] a = str.Split(',');
        
        x1 = int.Parse(a[0]);
        y1 = int.Parse(a[1]);
        time = int.Parse(a[2]);
        array.Add(new fruit_point(x1, y1, time,0));
        for (int i = 0; i < str.Length; i++)
        {
            
            if (str[i] == 'B' || str[i] == 'P' || str[i] == 'L' || str[i] == 'C')
            {
                a = str.Substring(i + 2).Split(':');
               
                x2 = int.Parse(a[0]);
                y2 = int.Parse(a[1].Split(',')[0].Split('|')[0]);
                a = str.Split(',');
                repeat = int.Parse(a[6]);
                length = int.Parse(a[7].Split('.')[0]);
                int n = Mathf.Abs(x2 - x1)/15;
                for(int j=1; j<3; j++)
                {
                    int dx = (x2 - x1)/3;
                    float  dt = (length * Fruit.speed / 8f) / 3f;
                    array.Add(new fruit_point(x1+dx*j, y2, (int)(time + dt*j), 1));
                }
                array.Add(new fruit_point(x2, y2, (int)(time+length*Fruit.speed/8f),0));
                break;
            }
        }
        


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
        lenY = max.y - min.y;
        maxY = max.y;
        apple = Resources.Load<Fruit> ("Fruit_apple");
		grape = Resources.Load<Fruit> ("Fruit_grape");
		orange = Resources.Load<Fruit> ("Fruit_orange");
		pear = Resources.Load<Fruit> ("Fruit_pear");
        drop = Resources.Load<Fruit>("drop");

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
            parse(str, ref array);
        }
        scale = new Vector3(0.4f+1/CircleSize, 0.4f+1/CircleSize, 1);
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
                    AudioLoad.audioSource.volume = 0.1f;
                    isPlaying = true;
                }

                Vector3 pos = Vector3.zero;
                pos.x = f.x * lenX / 512f - lenX / 2f;
                pos.y = maxY;
                if (f.type == 0)
                {

                    
                    switch (UnityEngine.Random.Range(1, 5))
                    {

                        case 1:

                            Instantiate(apple, pos, transform.rotation);
                            break;
                        case 2:

                            Instantiate(grape, pos, transform.rotation);
                            break;
                        case 3:

                            Instantiate(orange, pos, transform.rotation);
                            break;
                        case 4:

                            Instantiate(pear, pos, transform.rotation);
                            break;
                    }
                }
                if (f.type == 1)
                {
                    

                    Instantiate(drop, pos, transform.rotation);
                }

                    array.Remove(f);
                
                
            }
        }
    }
}
