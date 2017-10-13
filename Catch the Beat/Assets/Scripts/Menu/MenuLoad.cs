using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuLoad : MonoBehaviour {

    public static MenuItem[] maps;
  

    public static string folder;
    public static string map;
    private string[] mapsNames;
    public static float timeBegin=100000000;
    Image[] lists;
    ContentSizeFitter grid1;
    ContentSizeFitter grid2;
    string[] directories;
    Vector2 touch;

   private static int countOfMaps;

    void Start ()
    {
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        directories = Directory.GetDirectories(Application.persistentDataPath);
        int len = Application.persistentDataPath.Length+1;
        mapsNames = new string[directories.Length];
        maps = new MenuItem[directories.Length];
        lists = GetComponentsInChildren<Image>();


        Vector2 pos = max;

        Vector2 offset = gameObject.GetComponent<RectTransform>().offsetMax;
        pos = offset;
        pos.x = offset.x /8+ offset.x;
        pos.y = pos.y / 2;
        lists[0].transform.position = pos;
        pos.x = 25;
        pos.y = 0;
        lists[0].rectTransform.offsetMax = pos;

        grid1 = lists[1].GetComponentInChildren<ContentSizeFitter>();
        countOfMaps = 0;
        Canvas[] cs = GetComponentsInChildren<Canvas>();
        foreach (string s in directories)
        {
            mapsNames[countOfMaps] = s;
            maps[countOfMaps] = Instantiate(Resources.Load<MenuItem>("Menu/MenuItem"), grid1.gameObject.transform);
            maps[countOfMaps].initialize(s.Substring(len),this,cs[1]);
            countOfMaps++;
        }
        for (int i=0; i<maps.Length; i++)
        {
            if (maps[i].name() == folder)
            {
                maps[i].firstSelect();
                break;
            }
        }
        
    }
    public void select(string _name)
    {
       
        folder = _name;
        string path = Application.persistentDataPath + '/' + _name;
        int len = path.Length + 1;
        string[] directories = Directory.GetFiles(path, "*.osu");
        MenuSubItem[] subMaps = new MenuSubItem[directories.Length];
        MenuSubItem[] mp = grid1.gameObject.GetComponentsInChildren<MenuSubItem>();
        MenuItem[] mp1 = grid1.gameObject.GetComponentsInChildren<MenuItem>();
        for (int i = 0; i < mp.Length; i++)
        {
            Destroy(mp[i].gameObject);
        }
        for (int i = 0; i < mp1.Length; i++)
        {
            Destroy(mp1[i].gameObject);
        }
        Canvas[] cs = GetComponentsInChildren<Canvas>();
        int j = 0;
        foreach (string s in this.directories)
        {
            if (s.Substring(Application.persistentDataPath.Length + 1) == _name)
            {
                for (int i = 0; i < directories.Length; i++)
                {
                    subMaps[i] = Instantiate(Resources.Load<MenuSubItem>("Menu/MenuSubItem"), grid1.gameObject.transform);
                    subMaps[i].initialize(directories[i].Substring(len, directories[i].Length - len - 4), this);
                }
            }
            else
            {
                maps[j] = Instantiate(Resources.Load<MenuItem>("Menu/MenuItem"), grid1.gameObject.transform);
                maps[j].initialize(s.Substring(Application.persistentDataPath.Length + 1), this, cs[1]);
            }
            j++;
        }
       
    }
    public void selectMap(string _name)
    {
        map = _name+".osu";
        SceneManager.LoadScene("scene");
    }


    void Update () {

    }
}
