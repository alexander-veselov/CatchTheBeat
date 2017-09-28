using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AudioLoad : MonoBehaviour {

    public WWW www;
    public AudioClip myAudioClip;
    public string path;
    public static AudioSource audioSource;
    public static bool isLoaded = false;
    void Start () {
        path = Application.persistentDataPath + '/' + MenuLoad.folder + '/';
        string[] dir = Directory.GetFiles(path, "*.mp3");
        www = new WWW("file://"+ dir[0]);
        
        
        myAudioClip = www.GetAudioClip();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = myAudioClip;
    }
	
    public static void Play()
    {
    //    while(!(!audioSource.isPlaying && audioSource.clip.isReadyToPlay));
    //    {
            
    //       // MenuLoad.timeBegin = Time.time;
    //    }
    }
    bool f = true;
	void Update () {
        if (!audioSource.isPlaying && audioSource.clip.isReadyToPlay && f)
        {
            
            MenuLoad.timeBegin = Time.time;
            f = false;
            //Debug.Log(Time.time);
        }

    }
}
