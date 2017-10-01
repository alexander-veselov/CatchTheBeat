using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MapsLoad : MonoBehaviour
{


    Fruit fruit;
    Fruit drop;
    private Color32[] colors;


    public class fruit_point
    {
        public fruit_point(int X, int Y, int Time, Fruit.types _type, Color32 c)
        {
            type = _type;
            x = X;
            y = Y;
            color = c;
            time = Time;
        }
        public int time;
        public Fruit.types type;
        public Color32 color;
        public int x;
        public int y;
    }
    void parse(string str, ref ArrayList array)
    {
        int x1 = 0, x2 = 0, y1 = 0, y2 = 0, time = 0, repeat = 0, length = 0;
        string[] a = str.Split(',');

        x1 = int.Parse(a[0]);
        y1 = int.Parse(a[1]);
        time = int.Parse(a[2]);
        Color32 randColor = colors[UnityEngine.Random.Range(0, 4)];
        array.Add(new fruit_point(x1, y1, time, Fruit.types.FRUIT, randColor));
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

                for (int j = 0; j < repeat; j++)
                {
                    float t = time + length * Fruit.speed / 8f * j;
                    if (j % 2 == 0) createSlider(x1, x2, y1, y2, t, ref array, randColor, length);
                    else createSlider(x2, x1, y2, y1, t, ref array, randColor, length);
                }
                break;
            }
        }

    }
    private void createSlider(int x1, int x2, int y1, int y2, float time, ref ArrayList array, Color32 color, int length)
    {

        for (int j = 1; j < 3; j++)
        {
            int dx = (x2 - x1) / 3;
            float dt = (length * Fruit.speed / 8f) / 3f;
            array.Add(new fruit_point(x1 + dx * j, y2, (int)(time + dt * j), Fruit.types.DROP, color));
        }
        array.Add(new fruit_point(x2, y2, (int)(time + length * Fruit.speed / 8f), Fruit.types.FRUIT, color));
    }
    private AudioSource audioSource;
    private int count = 0;
    private ArrayList array;
    private StreamReader input;
    private String background;
    public static Canvas bg;
    public static float HPDrainRate;
    public static float CircleSize;
    public static float OverallDifficulty;
    public static float ApproachRate;

    private float lenX, lenY, maxY;
    public static Vector3 scale = new Vector3(1, 1, 1);

  
    private void Start()
    {
        colors = new Color32[4];
        colors[0] = new Color32(158, 47, 255, 255);
        colors[1] = new Color32(255, 76, 185, 255);
        colors[2] = new Color32(36, 166, 101, 255);
        colors[3] = new Color32(46, 132, 164, 255);

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        lenX = max.x - min.x - 3f * Player.sprite.size.x;
        lenY = max.y - min.y;
        maxY = max.y;

        fruit = Resources.Load<Fruit>("Prefabs/fruit");
        drop = Resources.Load<Fruit>("Prefabs/drop");

        input = File.OpenText(Application.persistentDataPath + '/' + MenuLoad.folder + '/' + MenuLoad.map);
        String str;
      
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
            if (str == "[Events]") break;
        }
        str = input.ReadLine();
        str = input.ReadLine();
        if (str[0]=='V') str = input.ReadLine();
        background = str.Split(',')[2];
        bgLoad(background.Substring(1, background.Length - 2));
        while ((str = input.ReadLine()) != null)
        {
            if (str == "[HitObjects]") break;
        }
        array = new ArrayList();
        while ((str = input.ReadLine()) != null)
        {
            parse(str, ref array);
        }

        scale = new Vector3(0.4f + 1 / CircleSize, 0.4f + 1 / CircleSize, 1);
        Fruit.speed = ApproachRate * max.y / 2.6f;
        fruit.transform.localScale = scale;
        Player.score = Instantiate(Resources.Load<playerScore>("Prefabs/Score"));
    }

    void bgLoad(string s)
    {
        SpriteRenderer image = bg.GetComponentInChildren<SpriteRenderer>();
        RectTransform rectTr = bg.GetComponentInChildren<RectTransform>();
        String path = Application.persistentDataPath + '/' + MenuLoad.folder + '/' + s;
        WWW www = new WWW("file://" + path);
        Texture2D tex;
        tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
        www.LoadImageIntoTexture(tex);
        image.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        image.transform.localScale = new Vector2(100f, 100f);
        image.color = new Color(1, 1, 1, 0.25f);
    }

    private bool isPlaying = false;
    void Update()
    {
        foreach (fruit_point f in array)
        {
            if (f.time <= ((Time.time - MenuLoad.timeBegin) * 1000))
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
                if (f.type == Fruit.types.FRUIT)
                {
                    Fruit newFruit = Instantiate(fruit, pos, transform.rotation);
                    newFruit.initialize(f.color, f.type);
                }
                if (f.type == Fruit.types.DROP)
                {
                    Fruit newDrop = Instantiate(drop, pos, transform.rotation);
                    newDrop.initialize(f.color, f.type);
                    newDrop.transform.localScale = scale;
                }
                array.Remove(f);
                break;
            }
        }
    }
}