using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MenuItem : MonoBehaviour {

    Text mapName;
    MenuLoad load;
    Canvas bg;
	void Start()
    {
       
    }
    public void initialize(string s, MenuLoad ml, Canvas c)
    {
        load = ml;
        bg = c;
        mapName = GetComponentInChildren<Text>();
        mapName.text = s;
    }
    void bgLoad()
    {
        string path = Application.persistentDataPath + '/' + mapName.text;
        
        int len = path.Length + 1;
        string[] directories = Directory.GetFiles(path, "*.jpg");
        if (directories.Length ==0)
        {
            directories = Directory.GetFiles(path, "*.png");
        }

        

        SpriteRenderer image = bg.GetComponentInChildren<SpriteRenderer>();
        RectTransform rectTr = bg.GetComponentInChildren<RectTransform>();
        
        WWW www = new WWW("file://" + directories[0]);
        Texture2D tex;
        tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
        www.LoadImageIntoTexture(tex);
        image.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        var width = image.sprite.bounds.size.x;
        var height = image.sprite.bounds.size.y;
        var worldScreenHeight = Camera.main.orthographicSize * 2.0;
        var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
        image.transform.localScale = new Vector2((float)worldScreenWidth / width,(float) worldScreenHeight / height);
        
        image.color = new Color(1, 1, 1, 0.5f);

        SpriteRenderer menuBG = GameObject.Find("menuBG").GetComponent<SpriteRenderer>();
        menuBG.transform.localScale = new Vector2(1, 1);

    }
    public void select()
    {

        bgLoad();
        load.select(mapName.text);
    }
	void Update () {
		
	}
}
