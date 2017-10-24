using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class records : MonoBehaviour {

    private StreamReader input;
    private StreamWriter wr;
    Stream s;
    public static ArrayList maps;
    void Start () {
        input = File.OpenText(Application.persistentDataPath + "/records.db");
        string inputText;
        maps = new ArrayList();
        while ((inputText = input.ReadLine()) != null)
        {
            maps.Add(inputText);
        }
        input.Close();
    }
    
    public void setRecord(string mapName,  long score, float accuracy)
    {
        bool isFound = false;
        for (int i=0; i<maps.Count; i++)
        {
            if ((string)maps[i] == mapName)
            {
                isFound = true;
                if (long.Parse((string)maps[i+1]) < score)
                {
                    maps[i + 1] = score.ToString();
                    maps[i + 2] = accuracy.ToString();
                    retype();                 
                    break;
                }
            }
            
        }
        if (!isFound)
        {
            maps.Add(mapName);
            maps.Add(score.ToString());
            maps.Add(accuracy.ToString());
            retype();
        }
    }

    void retype()
    {
        wr = new StreamWriter(Application.persistentDataPath + "/records.db", false);
        foreach (string s in maps) wr.WriteLine(s);
        wr.Close();
    }
	
	public static long getRecord(string mapName)
    {
        for (int i = 0; i < maps.Count; i++)
        {
            if ((string)maps[i] == mapName)
            {
                
                return long.Parse((string)maps[i + 1]);
            }

        }
        return 0;
    }
    public static float getAccuracy(string mapName)
    {
        for (int i = 0; i < maps.Count; i++)
        {
            if ((string)maps[i] == mapName)
            {

                return float.Parse((string)maps[i + 2]);
            }

        }
        return -1;
    }
    void Update () {
		
	}
}
