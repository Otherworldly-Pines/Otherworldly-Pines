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
    private bool isFading = false;

    public static MenuMusic GetInstance() {
        return FindObjectOfType<MenuMusic>();
    }

    public static void StartIfStopped() {
        var instance = GetInstance();
        if (!instance.menuAudioSource.isPlaying) {
            instance.StartPlaying();
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
        
        DontDestroyOnLoad(gameObject);
        
        StartIfStopped();
    }

    public void StartPlaying() {
        menuAudioSource.time = 0f;
        menuAudioSource.volume = 0f;
        menuAudioSource.Play();
        StartCoroutine(FadeAudioIn());
    }

    // Update is called once per frame
    void Update() {
        if (!isFading) menuAudioSource.volume = GameSettings.musicVolume;
        buttonClickSource.volume = GameSettings.sfxVolume;
    }

    IEnumerator FadeAudioIn() {
        var t = 0f;
        var duration = 2f;

        isFading = true;

        while (t < duration) {
            menuAudioSource.volume = SoundManager.SmoothLerp(0f, GameSettings.musicVolume, t / duration);
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        isFading = false;
    }

}
