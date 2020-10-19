using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class GravityRegion : MonoBehaviour {

    public bool gravityIsFlipped = false;
    public bool playerCanFlipGravity = true;
    public int maxFlipCount = 0; // Only considered restricted if greater than zero

    public SpriteRenderer background;

    private BoxCollider2D ownCollider;
    private HashSet<GravityFlippable> flippables = new HashSet<GravityFlippable>();
    private Color gravityColor = new Color(0f, 0.12f, 0.34f, 1f);
    private int currentFlipCount = 0;

    void Start() {
        ownCollider = GetComponent<BoxCollider2D>();
        if (background != null) {
            Vector3 updatedScale = new Vector3(ownCollider.size.x, ownCollider.size.y, 1f);
            background.transform.localScale = updatedScale;
            background.transform.localPosition = new Vector3(ownCollider.offset.x, ownCollider.offset.y, 0f);
        }
    }

    void Update() {
        if (background != null) {
            background.color = gravityIsFlipped ? gravityColor : Color.clear;
        }
    }

    public void FlipGravity() {
        if (!playerCanFlipGravity) return;
        if (maxFlipCount > 0 && currentFlipCount >= maxFlipCount) return;

        currentFlipCount++;
        gravityIsFlipped = !gravityIsFlipped;

        flippables.RemoveWhere(flippable => !flippable.StillExists());
        foreach (GravityFlippable flippable in flippables) {
            flippable.Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        GravityCenter gravityCenter = collider.GetComponent<GravityCenter>();
        if (gravityCenter == null) return;

        foreach (GravityAffected owner in gravityCenter.owners) {
            // If the player just entered the region, inform them of that so they can control it
            if (owner is GravityControl) {
                GravityControl controller = owner as GravityControl;
                controller.EnterGravityRegion(this);
            }

            // If a flippable object just entered, add them to this gravity region
            // and if they need their gravity updated, take care of that too
            if (owner is GravityFlippable) {
                GravityFlippable flippable = owner as GravityFlippable;
                flippables.Add(flippable);
                if (gravityIsFlipped != flippable.isUpsideDown) flippable.Flip();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider) {
        GravityCenter gravityCenter = collider.GetComponent<GravityCenter>();
        if (gravityCenter == null) return;

        foreach (GravityAffected owner in gravityCenter.owners) {
            // If the player just exited, inform them of that
            if (owner is GravityControl) {
                GravityControl controller = owner as GravityControl;
                controller.ExitGravityRegion(this);
            }

            // If a flippable object just exited, remove them from the list of flippabless
            if (owner is GravityFlippable) {
                GravityFlippable flippable = owner as GravityFlippable;
                flippables.Remove(flippable);
            }
        }
    }

    private void OnDrawGizmos() {
        if (!ownCollider) ownCollider = GetComponent<BoxCollider2D>();
        Gizmos.color = Color.blue;
        GizmosUtility.DrawCollider(ownCollider);
    }

    private void OnDrawGizmosSelected() {
        if (!ownCollider) ownCollider = GetComponent<BoxCollider2D>();
        Gizmos.color = Color.yellow;
        GizmosUtility.DrawCollider(ownCollider);
    }

    private bool IsValid(float size) {
        var rounded = Mathf.Round(size * 4f) / 4f;
        return Mathf.Abs(rounded - size) < 0.000001f;
    }

    private void OnValidate() {
        if (transform.position.z < 90f) Debug.LogError("Gravity regions must have z positions over 90 " + gameObject.name);
        if (!ownCollider) ownCollider = GetComponent<BoxCollider2D>();
        if (ownCollider.offset.magnitude > 0.001f) {
            var corrected = transform.position + (Vector3)ownCollider.offset;
            Debug.LogError("Gravity region colliders must have offsets of zero. " + gameObject.name + " position should be " + corrected);
        }

        if (!IsValid(ownCollider.size.x) || !IsValid(ownCollider.size.y))
            Debug.LogError("Gravity region collider sizes must be multiples of 0.25: " + gameObject.name);
    }

}