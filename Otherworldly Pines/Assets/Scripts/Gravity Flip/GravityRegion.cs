using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class GravityRegion : MonoBehaviour {

    [SerializeField] private ParticleSystem particles;
    [SerializeField] private BoxCollider2D boundsCollider;
    public bool gravityIsFlipped = false;
    public bool playerCanFlipGravity = true;

    private BoxCollider2D ownCollider;
    private HashSet<GravityFlippable> flippables = new HashSet<GravityFlippable>();

    void Awake() {
        if (!playerCanFlipGravity) particles.Stop();
        ownCollider = GetComponent<BoxCollider2D>();
        
        Vector3 updatedScale = new Vector3(ownCollider.size.x, ownCollider.size.y, 1f);
        boundsCollider.size = updatedScale;
        
        var sh = particles.shape;
        sh.scale = new Vector3(ownCollider.size.x, 1, ownCollider.size.y);
        particles.transform.localPosition = new Vector3(particles.transform.localPosition.x, 0, -0.5f);

        var emitter = particles.emission;
        emitter.rateOverTime = ownCollider.size.x * 2f;
    }

    private void Start() {
        ConfigureParticles();
    }

    // returns whether or not the region is flipped, publicly accessible function
    public bool getIsFlipped()
    {
        return gravityIsFlipped;
    }

    public void FlipGravity() {
        if (!playerCanFlipGravity) return;
        
        gravityIsFlipped = !gravityIsFlipped;
        
        ConfigureParticles();

        flippables.RemoveWhere(flippable => !flippable.StillExists());
        foreach (GravityFlippable flippable in flippables) {
            flippable.Flip();
        }
    }

    private void ConfigureParticles() {
        var localRotation = particles.transform.localRotation;
        localRotation.z = gravityIsFlipped ? 0f : 180f;
        particles.transform.localRotation = localRotation;
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
        if (transform.position.z < 90f) Debug.LogError("Gravity regions must have z positions over 90 ", gameObject);
        if (!ownCollider) ownCollider = GetComponent<BoxCollider2D>();
        if (ownCollider.offset.magnitude > 0.001f) {
            var corrected = transform.position + (Vector3)ownCollider.offset;
            Debug.LogError("Gravity region colliders must have offsets of zero. Position should be " + corrected, gameObject);
        }

        if (!IsValid(ownCollider.size.x) || !IsValid(ownCollider.size.y))
            Debug.LogError("Gravity region collider sizes must be multiples of 0.25", gameObject);
    }

}