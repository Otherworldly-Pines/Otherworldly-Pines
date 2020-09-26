using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityControl : GravityAffected
{

    private GravityRegion activeGravityRegion;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && activeGravityRegion != null)
        {
            activeGravityRegion.FlipGravity();
        }
    }

    public void EnterGravityRegion(GravityRegion region)
    {
        activeGravityRegion = region;
    }

    public void ExitGravityRegion(GravityRegion region)
    {
        if (activeGravityRegion.gameObject.GetInstanceID() == region.gameObject.GetInstanceID())
        {
            activeGravityRegion = null;
        }
    }
}
