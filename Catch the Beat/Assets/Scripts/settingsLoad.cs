using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;

public class settingsLoad : MonoBehaviour {

    string path;
    public static string dir;
    void Awake () {

        dir = Application.persistentDataPath;
        //dir = ((((Directory.GetCurrentDirectory()+"\\CatchTheBeat"))))";
        path = dir + "/" + "Panda Eyes & Teminite - Highscore/";

        bool isExist = File.Exists(dir + "\\records.db");
        if (!isExist)
        {
            Directory.CreateDirectory(dir);
            File.Create(dir + "//records.db");
            writeMapOnDisk();
        }
 
        
    }
    void writeMapOnDisk()
    {
        
        var texture = Resources.Load<Texture2D>("Panda Eyes & Teminite - Highscore\\BG");
        Directory.CreateDirectory(path);
        File.WriteAllBytes(path + "/BG.png", texture.EncodeToPNG());

        writeMap("Panda Eyes & Teminite - Highscore (Fort) [Hyper]");
        writeMap("Panda Eyes & Teminite - Highscore (Fort) [Game Over]");
        writeMap("Panda Eyes & Teminite - Highscore (Fort) [Standard]");
        TextAsset map = (TextAsset)Resources.Load("Panda Eyes & Teminite - Highscore\\song", typeof(TextAsset));
        File.WriteAllBytes(path + "/song.mp3", map.bytes);
        
    }

    void writeMap(string s)
    {
        StreamWriter wr;
        TextAsset map = (TextAsset)Resources.Load(("Panda Eyes & Teminite - Highscore\\"+s), typeof(TextAsset));
        Debug.Log(path + s + ".osu");
        wr = new StreamWriter(path + "/"+s+".osu", false);
        wr.Write(map.text);
        wr.Dispose();
        wr.Close();
    }
    void Update () {
		
	}
}
