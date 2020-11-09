using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    public Slider musicSlider;
    public Slider sfxSlider;
    
    // Start is called before the first frame update
    void Start() {
        musicSlider.value = GameSettings.musicVolume;
        sfxSlider.value = GameSettings.sfxVolume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetMusicVolume(float value) {
        GameSettings.musicVolume = value;
    }

    public void SetSfxVolume(float value) {
        GameSettings.sfxVolume = value;
    }
}
