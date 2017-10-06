using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mapEnd : MonoBehaviour {

    Image[] panel;
	void Start () {
        panel = GetComponentsInChildren<Image>();
        Vector2 max = new Vector2(Screen.width, Screen.height);
        max.x = max.y;
        panel[1].transform.localScale = max/450f;
        max.y = Screen.height/ 600f;
        panel[0].transform.localScale = max;
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        max.y /=1.6f;
        max.x *=(-1.01f);
        panel[1].transform.position = max;
        max = Camera.main.ViewportToWorldPoint(new Vector2(0, 1));
        panel[0].transform.position = max;

        Vector3 p = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        p.z = 0;
        panel[2].transform.position = p;
        p = new Vector3(Screen.width, Screen.height,1);
        p.y = Screen.height / 600f;
        p.x = p.y;
        panel[2].transform.localScale = p;
        bgLoad();
    }
    void bgLoad()
    {
        string s = MapsLoad.currentMap;
        SpriteRenderer image = GameObject.Find("BG").GetComponent<SpriteRenderer>();
        String path = Application.persistentDataPath + '/' + MenuLoad.folder + '/' + s;
        WWW www = new WWW("file://" + path);
        Texture2D tex;
        tex = new Texture2D(4, 4, TextureFormat.DXT1, false);
        www.LoadImageIntoTexture(tex);
        image.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        var width = image.sprite.bounds.size.x;
        var height = image.sprite.bounds.size.y;
        var worldScreenHeight = Camera.main.orthographicSize * 2.0;
        var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
        image.transform.localScale = new Vector2((float)worldScreenWidth / width, (float)worldScreenHeight / height);
        image.color = new Color(1, 1, 1, 0.5f);
    }
  
    // Update is called once per frame
    void Update () {
		
	}
}
