using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AudioLoad : MonoBehaviour {

    private WWW www;
    private AudioClip myAudioClip;
    public string path;
    public static AudioSource audioSource;
    public static bool isLoaded = false;
    private bool isNotPlaying = true;

    void Start ()
    {
        load();
    }
    void load()
    {
        path = Application.persistentDataPath + '/' + MenuLoad.folder + '/';
        string[] dir = Directory.GetFiles(path, "*.mp3");
        www = new WWW("file://" + dir[0]);
        myAudioClip = www.GetAudioClip();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = myAudioClip;
    }
    
	void Update () {
        if (!audioSource.isPlaying && audioSource.clip.isReadyToPlay && isNotPlaying)
        {
            MenuLoad.timeBegin = Time.time;
            isNotPlaying = false;
        }

    }
}
