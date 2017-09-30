using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuLoad : MonoBehaviour {

    public static MenuItem[] maps;
    public static MenuSubItem[] subMaps;

    public static string folder;
    public static string map;
    private string[] mapsNames;
    public static float timeBegin=100000000;

   private static int countOfMaps;

    void Start ()
    {
        string[] directories = Directory.GetDirectories(Application.persistentDataPath);
        int len = Application.persistentDataPath.Length+1;
        mapsNames = new string[directories.Length];
        maps = new MenuItem[directories.Length];
        subMaps = new MenuSubItem[10];
        for (int i = 0; i < 10; i++)
        {
            subMaps[i] = gameObject.AddComponent<MenuSubItem>();
            Vector3 p = Vector3.zero;
            p.Set(5, -i + 2.25F, 0);
            subMaps[i].ini(p);
            subMaps[i].setEnactive();
            
        }
        countOfMaps = 0;
        foreach (string s in directories)
        {
      
                mapsNames[countOfMaps] = s;
                
                Vector3 p = transform.position;
                p.Set(-5, countOfMaps*2-2.2F, 0);
                
                maps[countOfMaps] = gameObject.AddComponent<MenuItem>();
                maps[countOfMaps].ini(p);
                maps[countOfMaps].setName(s.Substring(len));
                countOfMaps++;
            
        }
         

    }
    public static void select(string _name)
    {
        folder = _name;
        string path = Application.persistentDataPath + '/' + _name;
        int len = path.Length + 1;
        string[] directories = Directory.GetFiles(path, "*.osu");
        for(int i=0; i<directories.Length; i++)
        {
            subMaps[i].setActive(directories[i].Substring(len));
        }
        for(int i= directories.Length; i<10; i++)
        {
            subMaps[i].setEnactive();
        }



        for (int i = 0; i < countOfMaps; i++)
        {
            if (maps[i].getName() == _name)
            {
                maps[i].setColor(Color.magenta);
            }
            else
            {
                maps[i].setColor(Color.black);
            }
        }
    }
    public static void selectMap(string _name)
    {
        map = _name;
        SceneManager.LoadScene("scene");
    }


    void Update () {
		
	}
}
