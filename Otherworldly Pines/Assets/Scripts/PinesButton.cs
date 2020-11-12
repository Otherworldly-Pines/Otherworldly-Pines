using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinesButton : MonoBehaviour {

    private AudioSource instance;

    public static AudioSource GetSoundInstance() {
        var obj = GameObject.FindGameObjectWithTag("ButtonClickSoundSource");
        if (obj == null) return null;
        else return obj.GetComponent<AudioSource>();
    }

    private void Start() {
        instance = GetSoundInstance();
        if (!instance) {
            Debug.LogWarning("Button could not find the shared sound instance", gameObject);
            return;
        }
        
        DontDestroyOnLoad(instance.gameObject);
        GetComponent<Button>().onClick.AddListener(PlayClick);
    }

    public void PlayClick() {
        instance.Play();
    }
    
}
