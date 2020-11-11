using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    private AudioSource menuAudioSource;
    private AudioSource buttonClickSource;
    [SerializeField] private AudioClip buttonClick;
    // Start is called before the first frame update
    void Start()
    {
        var clickAudioObj = new GameObject();
        menuAudioSource = GetComponent<AudioSource>();
        buttonClickSource = clickAudioObj.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        menuAudioSource.volume = GameSettings.musicVolume;
        buttonClickSource.volume = GameSettings.sfxVolume;
    }

    public void playButtonClick()
    {
        buttonClickSource.PlayOneShot(buttonClick);
    }
}
