using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Sprite spriteActivated;
    private AudioSource audioSrc;
    private SpriteRenderer sr;
    private bool hasReached;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        audioSrc = GetComponent<AudioSource>();
        hasReached = false;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasReached) {
            hasReached = true;
            
            var player = other.gameObject;
            var berries = player.GetComponent<PlayerThrow>().getAmmo();
        
            CheckpointMaster.spawnPoint = gameObject.transform.position;
            CheckpointMaster.spawnAmmo = berries;
            CheckpointMaster.shouldResetToStartingPos = false;

            sr.sprite = spriteActivated;
            audioSrc.Play();
        }
    }

    private void OnValidate() {
        if (transform.position.z < 10f) {
            Debug.LogError("Checkpoints should have a z position of at least 10", gameObject);
        }
    }

}