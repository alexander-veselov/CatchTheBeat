using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AudioLoad : MonoBehaviour {
    [SerializeField]
    MapsLoad mapLoad;
    private WWW www;
    private AudioClip myAudioClip;
    public string path;
    public static bool fromBegin = true;
    public static AudioSource audioSource;
    public static bool isLoaded = false;
    public bool isNotPlaying = true;

    void Start ()
    {
        if (mapLoad.loadType == 0) load();
    }
    public void load()
    {
        path = Application.persistentDataPath + '/' + MenuLoad.folder + '/';
        isNotPlaying = true;
        string[] dir = Directory.GetFiles(path, "*.mp3");

        // Start the coroutine to load the audio
        StartCoroutine(LoadAudioClip(dir[0]));
    }

    private IEnumerator LoadAudioClip(string filePath)
    {
        www = new WWW("file://" + filePath);

        // Wait until the audio clip is fully loaded
        yield return www;

        // Check for any errors
        if (string.IsNullOrEmpty(www.error))
        {
            // Assign the loaded audio clip to the AudioSource
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = www.GetAudioClipCompressed();

            isLoaded = true;
            Debug.Log("Audio loaded successfully.");
        }
        else
        {
            Debug.LogError("Failed to load audio: " + www.error);
        }
    }

    public void stop()
    {
        audioSource.Stop();
    }
    
	  void Update () {
        if (isLoaded && !audioSource.isPlaying && audioSource.clip.isReadyToPlay && isNotPlaying)
        {
            MenuLoad.timeBegin = Time.time;
            isNotPlaying = false;
            if (fromBegin)
            {
                audioSource.time = 0;
            }
            else
            {
                audioSource.time = MapsLoad.PreviewTime / 1000.0f;
            }
            if (MapsLoad.DT)
            {
                audioSource.pitch = 1.4f;
            }
            else
            {
                audioSource.pitch = 1;
            }

            audioSource.Play();
            //audioSource.volume = 0.3f;
        }

    }
}
