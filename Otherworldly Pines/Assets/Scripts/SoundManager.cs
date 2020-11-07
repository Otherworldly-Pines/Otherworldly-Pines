using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip flipped_music;

    [SerializeField] private AudioClip clip_basic;
    [SerializeField] private AudioClip clip_collection;
    [SerializeField] private AudioClip clip_hurt;
    [SerializeField] private AudioClip clip_death;
    [SerializeField] private AudioClip clip_gravflip;

    [SerializeField] private AudioSource soundSource;

    private GravityRegion activeGravityRegion;
    private AudioClip unflipped_music;
    private bool isFlipped;

    private int timeStamp;

    // Start is called before the first frame update
    void Awake()
    {
        unflipped_music = soundSource.clip;
    }
    void Update()
    {
        timeStamp = soundSource.timeSamples;
    }

    public void SetActiveGravityRegion(GravityRegion gr)
    {
        activeGravityRegion = gr;
    }

    public void PlayCollectible() 
    {
        soundSource.PlayOneShot(clip_collection);
    }

    public void PlayBasic()
    {
        soundSource.PlayOneShot(clip_basic);
    }

    public void PlayHurt()
    {
        soundSource.PlayOneShot(clip_hurt);
    }

    public void PlayDead()
    {
        soundSource.PlayOneShot(clip_death);
    }

    public void PlayGravity()
    {
        soundSource.PlayOneShot(clip_gravflip);
    }

    public void SwapMusic()
    {
        isFlipped = activeGravityRegion.getIsFlipped();
        if (activeGravityRegion.getIsFlipped()) {
            soundSource.clip = flipped_music;
            soundSource.timeSamples = timeStamp;
            soundSource.Play();
        }
        else
        {
            soundSource.clip = unflipped_music;
            soundSource.timeSamples = timeStamp;
            soundSource.Play();
        }
    }
}
