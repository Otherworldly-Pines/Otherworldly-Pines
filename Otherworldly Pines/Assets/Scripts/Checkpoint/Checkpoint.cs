using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Sprite spriteActivated;
    private SpriteRenderer sr;
    private bool hasReached;
    private CheckpointMaster cm;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cm.lastCheckPointPos = gameObject.transform.position;
            if (!hasReached)
            {
                hasReached = true;
                sr.sprite = spriteActivated;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cm = GameObject.FindGameObjectWithTag("CM").GetComponent<CheckpointMaster>();
        sr = GetComponent<SpriteRenderer>();
        hasReached = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}