using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public int c1 = 2;
    public int c2 = 1;
    private int c1Collected = 0; // num of C1 collected
    private int c2Collected = 0; // num of C2 collected

    void Update()
    {
        if (c1 == c1Collected)
        {
            // show all C1 are collected
        }

        if (c2 == c2Collected)
        {
            // show all C2 are collected
        }
    }
    
    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Collectible1"))
        {
            Destroy(target.gameObject);
            c1Collected++;
            // show c1Collected/c1 are collected
        }
        else if (target.gameObject.CompareTag("Collectible2"))
        {
            Destroy(target.gameObject);
            c2Collected++;
            // show c2Collected/c2 are collected
        }
    }
}
