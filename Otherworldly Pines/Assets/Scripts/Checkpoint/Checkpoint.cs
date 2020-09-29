using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private CheckpointMaster cm;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cm.lastCheckPointPos = gameObject.transform.position;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cm = GameObject.FindGameObjectWithTag("CM").GetComponent<CheckpointMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}