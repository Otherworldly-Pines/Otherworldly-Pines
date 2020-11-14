using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezable : MonoBehaviour {

    public bool isFrozen = false;
    private PlayerFreeze player;
    private Rigidbody2D body;
    private Collider2D collider;
    private RigidbodyType2D originalBodyType;
    private SpriteRenderer renderer;
    private Color originalColor;

    void Start() {
        collider = GetComponent<Collider2D>();
        renderer = GetComponent<SpriteRenderer>();
        originalColor = renderer.color;

        body = GetComponent<Rigidbody2D>();
        originalBodyType = body.bodyType;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFreeze>();
    }

    public void Freeze() {
        isFrozen = true;
        body.bodyType = RigidbodyType2D.Static;
        renderer.color = Color.Lerp(originalColor, Color.cyan, 0.5f);
    }

    public void Unfreeze() {
        body.bodyType = originalBodyType;
        body.velocity = Vector2.zero;
        isFrozen = false;
        renderer.color = originalColor;
    }

    private void OnMouseEnter() {
        player.StartHovering(this);
        if (!isFrozen && player.CanFreeze()) renderer.color = Color.Lerp(originalColor, Color.cyan, 0.2f);
    }

    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(1)) {
            if (isFrozen) player.UnfreezeObject(this);
            else player.FreezeObject(this);
        }
    }

    private void OnMouseExit() {
        player.StopHovering(this);
        if (!isFrozen) renderer.color = originalColor;
    }

}