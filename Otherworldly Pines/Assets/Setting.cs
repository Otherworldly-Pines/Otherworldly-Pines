using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audios;

public class Settings : MonoBehaviour
{

    public AudioMixer audioMixer;

    public void setVolume(float volume)
    {
        audioMixer.SetFloat("Main Volume", volume);
    }
}
