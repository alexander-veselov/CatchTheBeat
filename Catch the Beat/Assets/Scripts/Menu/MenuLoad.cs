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
    MenuSubItem[] subMaps;
    string[] directories;
    Vector2 touch;
    string _name;


   private static int countOfMaps;

    void Start ()
    {
        GameObject.Find("mapScript").GetComponent<MapsLoad>().Logo = GameObject.Find("UI").GetComponent<logo>();
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        directories = Directory.GetDirectories(Application.persistentDataPath);
        int len = Application.persistentDataPath.Length+1;
        mapsNames = new string[directories.Length];
        maps = new MenuItem[directories.Length];
        lists = GetComponentsInChildren<Image>();


        Vector2 pos = max;
        lists[0].rectTransform.offsetMax = new Vector2(70f*max.x, -20f*max.y);
        lists[0].rectTransform.offsetMin = new Vector2(Screen.width/2f,max.y*25f);

        grid1 = lists[1].GetComponentInChildren<ContentSizeFitter>();
        countOfMaps = 0;
        Canvas[] cs = GetComponentsInChildren<Canvas>();
        foreach (string s in directories)
        {
            mapsNames[countOfMaps] = s;
            maps[countOfMaps] = Instantiate(Resources.Load<MenuItem>("Menu/MenuItem"), grid1.gameObject.transform);
            maps[countOfMaps].initialize(s.Substring(len),this,cs[0]);
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
        subMaps = new MenuSubItem[directories.Length];
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
        Canvas cs = GetComponentInChildren<Canvas>();
        int j = 0;
        foreach (string s in this.directories)
        {
            if (s.Substring(Application.persistentDataPath.Length + 1) == _name)
            {
                for (int i = 0; i < directories.Length; i++)
                {
                    subMaps[i] = Instantiate(Resources.Load<MenuSubItem>("Menu/MenuSubItem"), grid1.gameObject.transform);
                    subMaps[i].initialize(directories[i].Substring(len, directories[i].Length - len - 4), this,i);
                }
            }
            else
            {
                maps[j] = Instantiate(Resources.Load<MenuItem>("Menu/MenuItem"), grid1.gameObject.transform);
                maps[j].initialize(s.Substring(Application.persistentDataPath.Length + 1), this, cs);
            }
            j++;
        }
        selectDifficult(UnityEngine.Random.Range(0, subMaps.Length));
    }
    public void selectDifficult(int pos)
    {
        MenuLoad.map = subMaps[pos].GetComponentInChildren<MenuSubItem>().mapName+".osu";
        for (int i=0; i< subMaps.Length; i++)
        {
            subMaps[i].GetComponentInChildren<Image>().color = MenuItem.averageColor;
            subMaps[i].GetComponent<RectTransform>().transform.localEulerAngles = new Vector3(0, 18, 0);
        }
        subMaps[pos].GetComponentInChildren<Image>().color = new Color(1,1,1,0.75f);
        subMaps[pos].GetComponent<RectTransform>().transform.localEulerAngles = new Vector3(0, 0, 0);
        _name = subMaps[pos].GetComponentInChildren<MenuSubItem>().mapName;
    }
    public void selectMap()
    {
        GameObject.Find("sounds").GetComponent<sounds>().MenuHit();
        map = _name+".osu";
        AudioLoad.fromBegin = true;
        GameObject.Find("mapScript").GetComponent<MapsLoad>().fileParse();
        GameObject.Find("mapScript").GetComponent<MapsLoad>().bitLoad();
        SceneManager.LoadScene("scene");
    }


    void Update () {

    }
}
