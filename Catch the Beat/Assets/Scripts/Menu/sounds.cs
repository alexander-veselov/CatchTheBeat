using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sounds : MonoBehaviour {

    [SerializeField]
    public AudioClip menuClick;
    public AudioClip failsound;
    public AudioClip menuBack;
    public AudioClip mapEnd;
    public AudioClip menuHit;
    public AudioClip pause;
    public AudioClip modClap;
    public AudioSource source;
    void Start ()
    {

    }
    public void MenuClick()
    {
        source.clip = menuClick;
        source.Play();
    }
    public void Failsound()
    {
        source.clip = failsound;
        source.Play();
    }
    public void MenuBack()
    {
        source.clip = menuBack;
        source.Play();
    }
    public void MapEnd()
    {
        source.clip = mapEnd;
        source.Play();
    }
    public void MapEndOff()
    {
        source.Stop();
    }
    public void MenuHit()
    {
        source.clip = menuHit;
        source.Play();
    }
    public void Pause()
    {
        source.clip = pause;
        source.Play();
    }
    public void PauseOff()
    {
        source.Stop();
    }
    public void ModClap()
    {
        source.clip = modClap;
        source.Play();
    }

    void Update () {
		
	}
}
