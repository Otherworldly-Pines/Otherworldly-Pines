using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    
    public static float musicTime = 0f;
    
    protected AudioSource menuAudioSource;
    private AudioSource buttonClickSource;
    [SerializeField] private AudioClip buttonClick;

    public static MenuMusic GetInstance() {
        return FindObjectOfType<MenuMusic>();
    }

    public static void StartIfStopped() {
        var instance = GetInstance();
        if (!instance.menuAudioSource.isPlaying) {
            instance.menuAudioSource.time = 0f;
            instance.menuAudioSource.Play();
        }
    }

    public static void StopPlaying() {
        var instance = GetInstance();
        if (instance == null) return;
        instance.menuAudioSource.Stop();
    }

    void Start() {
        if (GetInstance().GetInstanceID() != GetInstanceID()) Destroy(gameObject);

        gameObject.tag = "MenuAudio";
        var clickAudioObj = new GameObject();
        clickAudioObj.transform.parent = transform;
        menuAudioSource = GetComponent<AudioSource>();
        buttonClickSource = clickAudioObj.AddComponent<AudioSource>();

        if (!menuAudioSource.isPlaying) menuAudioSource.Play();
        
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        menuAudioSource.volume = GameSettings.musicVolume;
        buttonClickSource.volume = GameSettings.sfxVolume;
    }

}
