using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuItem : MonoBehaviour {

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
        gameObj = Instantiate(Resources.Load<GameObject>("Item"), pos, transform.rotation);
        canvas = gameObj.GetComponentInChildren<Canvas>();
        position = pos;
        rend = gameObj.GetComponentInChildren<SpriteRenderer>();
        rend.color = Color.black;
        
    }

    public void setName(string _name)
    {
        mapName = _name;
        canvas.GetComponentInChildren<Text>().text = _name;
    }
    public string getName()
    {
        return  mapName;
    }
    public void setColor(Color _col)
    {
        rend.color = _col;
    }
    public void select()
    {
        MenuLoad.select(this.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>().text);
    }
	void Update () {
		
	}
}
