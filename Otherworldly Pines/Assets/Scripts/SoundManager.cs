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
    [SerializeField] private AudioClip clip_gravflip_ready;
    [SerializeField] private AudioClip clip_gravflip_up;
    [SerializeField] private AudioClip clip_gravflip_down;
    [SerializeField] private AudioClip clip_gravflip_unavailable;
    [SerializeField] private AudioClip buttonClick;
    private AudioSource unflippedMusicSource;
    private AudioSource flippedMusicSource;

    [SerializeField] private AudioSource soundSource;

    private GravityRegion activeGravityRegion;
    private AudioClip unflipped_music;

    // Start is called before the first frame update
    void Awake()
    {
        // Create audio sources
        var as1obj = new GameObject();
        as1obj.transform.parent = transform;
        unflippedMusicSource = as1obj.AddComponent<AudioSource>();
        unflippedMusicSource.loop = true;

        var as2obj = new GameObject();
        as2obj.transform.parent = transform;
        flippedMusicSource = as2obj.AddComponent<AudioSource>();
        flippedMusicSource.loop = true;

        unflipped_music = soundSource.clip;
        unflippedMusicSource.clip = unflipped_music;
        flippedMusicSource.clip = flipped_music;
        soundSource.clip = null;

        unflippedMusicSource.volume = GameSettings.musicVolume;
        flippedMusicSource.volume = GameSettings.musicVolume;
        soundSource.volume = GameSettings.sfxVolume;

        unflippedMusicSource.Play();
        flippedMusicSource.Play();
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

    public void PlayGravflipUp()
    {
        soundSource.PlayOneShot(clip_gravflip_up);
    }
    public void PlayGravflipDown()
    {
        soundSource.PlayOneShot(clip_gravflip_down);
    }
    public void PlayGravflipReady()
    {
        soundSource.PlayOneShot(clip_gravflip_ready);
    }
    public void PlayGravflipUnavailable()
    {
        soundSource.PlayOneShot(clip_gravflip_unavailable);
    }

    public void playButtonClick()
    {
        soundSource.PlayOneShot(buttonClick);
        Debug.Log("button clicked");
    }

    public void SwapMusic()
    {
        if (activeGravityRegion != null && activeGravityRegion.gravityIsFlipped)
        {
            flippedMusicSource.volume = GameSettings.musicVolume;
            unflippedMusicSource.volume = 0f;
        }
        else
        {
            flippedMusicSource.volume = 0f;
            unflippedMusicSource.volume = GameSettings.musicVolume;
        }
    }

    public void UpdateMusicVolume()
    {
        SwapMusic();
    }

    public void UpdateSfxVolume()
    {
        soundSource.volume = GameSettings.sfxVolume;
    }
}
