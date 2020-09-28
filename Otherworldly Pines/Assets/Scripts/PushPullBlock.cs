using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPullBlock : MonoBehaviour {

    [HideInInspector] public bool shouldFreezePosition = false;
    [HideInInspector] public bool isBeingMoved = false;
    [HideInInspector] public Collider2D collider;

    public PhysicsMaterial2D highFrictionMaterial;
    private PhysicsMaterial2D originalMaterial;
    
    void Start() {
        collider = GetComponent<Collider2D>();
        originalMaterial = collider.sharedMaterial;
    }
    
    public void ResetFriction() {
        collider.sharedMaterial = originalMaterial;
    }

    public void FreezeInPlace() {
        collider.sharedMaterial = highFrictionMaterial;
    }
    
}
