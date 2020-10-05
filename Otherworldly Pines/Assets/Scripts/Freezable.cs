using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezable : MonoBehaviour {

    private PlayerFreeze player;
    private Rigidbody2D body;
    private RigidbodyType2D originalBodyType;

    void Start() {
        body = GetComponent<Rigidbody2D>();
        originalBodyType = body.bodyType;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFreeze>();
    }

    public void Freeze() {
        body.bodyType = RigidbodyType2D.Static;
    }

    public void Unfreeze() {
        body.bodyType = originalBodyType;
        body.velocity = Vector2.zero;
    }

    private void OnMouseDown() {
        player.FreezeObject(this);
    }

    private void OnMouseEnter() {
        player.StartHovering(this);
    }

    private void OnMouseExit() {
        player.StopHovering(this);
    }

}