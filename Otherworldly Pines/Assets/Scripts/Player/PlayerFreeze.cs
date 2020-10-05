using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeze : MonoBehaviour {

    private static float MAX_TIMEOUT = 10f;
    
    private HashSet<Freezable> hoveredObjects = new HashSet<Freezable>();
    private Freezable frozen;
    public bool hasFrozen = false;
    private float freezeTimeout = 0f;

    private void Update() {
        if (hasFrozen) {
            freezeTimeout -= Time.deltaTime;
            if (freezeTimeout < 0) {
                hasFrozen = false;
                frozen.Unfreeze();
            }
        }
    }

    public bool IsHoveringAny() {
        return hoveredObjects.Count > 0;
    }

    public void StartHovering(Freezable obj) {
        hoveredObjects.Add(obj);
    }

    public void StopHovering(Freezable obj) {
        hoveredObjects.Remove(obj);
    }

    public void FreezeObject(Freezable obj) {
        obj.Freeze();
        frozen = obj;
        hasFrozen = true;
        freezeTimeout = MAX_TIMEOUT;
    }

    public void UnfreezeObject(Freezable obj) {
        obj.Unfreeze();
        frozen = null;
        hasFrozen = false;
    }

}
