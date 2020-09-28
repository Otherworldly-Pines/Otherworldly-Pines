using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityControl : GravityAffected {

    public GravityFlipIndicator indicator;
    private GravityRegion activeGravityRegion;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && activeGravityRegion != null)
        {
            activeGravityRegion.FlipGravity();
        }
    }

    private void SetActiveGravityRegion(GravityRegion region) {
        activeGravityRegion = region;
        indicator.GravityRegionChanged(activeGravityRegion);
    }

    public void EnterGravityRegion(GravityRegion region) {
        SetActiveGravityRegion(region);
    }

    public void ExitGravityRegion(GravityRegion region)
    {
        if (activeGravityRegion.gameObject.GetInstanceID() == region.gameObject.GetInstanceID()) {
            SetActiveGravityRegion(null);
        }
    }
}
