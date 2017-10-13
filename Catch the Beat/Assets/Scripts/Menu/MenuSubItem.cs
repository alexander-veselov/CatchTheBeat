using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSubItem : MonoBehaviour {

    Text mapName;
    MenuLoad load;
    public void initialize(string s, MenuLoad ml)
    {
        load = ml;
        mapName = GetComponentInChildren<Text>();
        mapName.text = s;
    }
    public void selectMap()
    {
        GameObject.Find("bgMusic").GetComponent<AudioLoad>().load();
        load.selectMap(mapName.text);
    }

}
