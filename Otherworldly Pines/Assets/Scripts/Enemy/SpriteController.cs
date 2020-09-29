using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    private SpriteRenderer controller;
    // Start is called before the first frame update
    void Start()
    {
        this.controller = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void turnRight(){
        this.controller.flipX = true;
    }

    public void turnLeft(){
        this.controller.flipX = false;
    }
}
