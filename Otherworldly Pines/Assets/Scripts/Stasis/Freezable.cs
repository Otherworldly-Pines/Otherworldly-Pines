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
    public Sprite indicatorSprite;
    private SpriteRenderer renderer;

    void Start() {
        collider = GetComponent<Collider2D>();
        
        var rendererObj = new GameObject();
        rendererObj.transform.parent = transform;
        rendererObj.transform.localPosition = Vector3.zero;
        renderer = rendererObj.AddComponent<SpriteRenderer>();
        renderer.sprite = indicatorSprite;
        renderer.size = collider.bounds.size;
        renderer.color = Color.clear;

        body = GetComponent<Rigidbody2D>();
        originalBodyType = body.bodyType;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFreeze>();
    }

    public void Freeze() {
        isFrozen = true;
        body.bodyType = RigidbodyType2D.Static;
        renderer.color = Color.Lerp(Color.clear, Color.cyan, 0.5f);
    }

    public void Unfreeze() {
        body.bodyType = originalBodyType;
        body.velocity = Vector2.zero;
        isFrozen = false;
        renderer.color = Color.clear;
    }

    private void OnMouseDown() {
        if (isFrozen) player.UnfreezeObject(this);
        else player.FreezeObject(this);
    }

    private void OnMouseEnter() {
        player.StartHovering(this);
        if (!isFrozen && player.CanFreeze()) renderer.color = Color.Lerp(Color.clear, Color.cyan, 0.2f);
    }

    private void OnMouseExit() {
        player.StopHovering(this);
        if (!isFrozen) renderer.color = Color.clear;
    }

}