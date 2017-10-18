using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MenuItem : MonoBehaviour {

    string mapName;
    Text[] labels;
    MenuLoad load;
    Canvas bg;
	void Start()
    {
       
    }
    public void initialize(string s, MenuLoad ml, Canvas c)
    {
        load = ml;
        bg = c;
        mapName = s;
        labels = GetComponentsInChildren<Text>();
        string[] song = mapName.Split('-');
        if (song.Length == 1)
        {
            Destroy(gameObject);
            return;
        }
        labels[0].text = song[1].Substring(1);
        labels[1].text = song[0];
    }
    void bgLoad()
    {
        string path = Application.persistentDataPath + '/' + mapName;
        
        int len = path.Length + 1;
        string[] directories = Directory.GetFiles(path, "*.jpg");
        if (directories.Length ==0)
        {
            directories = Directory.GetFiles(path, "*.png");
        }

        

        SpriteRenderer image = GameObject.Find("Background").GetComponent<SpriteRenderer>();

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
        
        image.color = new Color(1, 1, 1, 0.6f);

        SpriteRenderer menuBG = GameObject.Find("menuBG").GetComponent<SpriteRenderer>();
        image.transform.position = new Vector3(0, 0, 100);
        menuBG.transform.localScale = new Vector2(1, 1);

    }
    public string name()
    {
        return mapName;
    }
    public void select()
    {
        firstSelect();
        AudioLoad.fromBegin = false;
        GameObject.Find("bgMusic").GetComponent<MapsLoad>().fileParse();
        GameObject.Find("bgMusic").GetComponent<MapsLoad>().bitLoad();
        GameObject.Find("bgMusic").GetComponent<AudioLoad>().load();
        
    }
    public void firstSelect()
    {
        bgLoad();
        load.select(mapName);
    }
    void Update () {
		
	}
}
