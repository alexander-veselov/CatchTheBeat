using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSubItem : MonoBehaviour {

    private GameObject gameObj;
    private string mapName;
    private Canvas canvas;
    private Vector3 position;
    private SpriteRenderer rend;

    void Start()
    {

    }

    public void ini(Vector3 pos)
    {
        gameObj = Instantiate(Resources.Load<GameObject>("Menu/SubItem"), pos, transform.rotation);
        canvas = gameObj.GetComponentInChildren<Canvas>();
        position = pos;
        rend = gameObj.GetComponentInChildren<SpriteRenderer>();
        rend.color = Color.black;
    }

    public void setActive(string _name)
    {
        rend.enabled = true;
        mapName = _name;
        canvas.GetComponentInChildren<Text>().text = _name;
    }
    public void setEnactive()
    {
        rend.enabled = false;
        canvas.GetComponentInChildren<Text>().text = "";
    }
    public void selectMap()
    {
        MenuLoad.selectMap(this.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>().text);
    }
    void Update()
    {

    }
}
