using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeze : MonoBehaviour, IHUDConnected {

    private static float MAX_TIMEOUT = 10f;
    
    private HashSet<Freezable> hoveredObjects = new HashSet<Freezable>();
    private Freezable frozen;
    public bool hasFrozen = false;
    private float freezeTimeout = MAX_TIMEOUT;
    private StasisDisplay stasisDisplay;

    private void Update() {
        if (hasFrozen) {
            freezeTimeout -= Time.deltaTime;
            if (freezeTimeout < 0) {
                hasFrozen = false;
                frozen.Unfreeze();
            }
        } else {
            if (freezeTimeout < MAX_TIMEOUT) freezeTimeout += Time.deltaTime;
            else if (freezeTimeout > MAX_TIMEOUT) freezeTimeout = MAX_TIMEOUT;
        }
        
        stasisDisplay.SetPercent(freezeTimeout / MAX_TIMEOUT);
    }

    public bool CanFreeze() {
        return freezeTimeout >= MAX_TIMEOUT;
    }

    public bool IsHoveringAny() {
        return hoveredObjects.Count > 0;
    }

    public void StartHovering(Freezable obj) {
        if (CanFreeze()) hoveredObjects.Add(obj);
    }

    public void StopHovering(Freezable obj) {
        hoveredObjects.Remove(obj);
    }

    public void FreezeObject(Freezable obj) {
        if (CanFreeze()) {
            obj.Freeze();
            frozen = obj;
            hasFrozen = true;
            freezeTimeout = MAX_TIMEOUT;
        }
    }

    public void UnfreezeObject(Freezable obj) {
        obj.Unfreeze();
        frozen = null;
        hasFrozen = false;
    }

    public void ConnectToHUD(HUD hud) {
        stasisDisplay = hud.stasisDisplay;
    }

}
