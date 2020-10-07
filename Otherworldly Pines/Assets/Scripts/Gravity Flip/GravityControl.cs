using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityControl : GravityAffected, IHUDConnected {

    private GravityFlipIndicator indicator;
    private GravityRegion activeGravityRegion;
    private PlayerControls player;
    private bool canFlip = true;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && isFlipActive())
        {
            activeGravityRegion.FlipGravity();
            player.setJumping(false);
        }
    }

    private bool isFlipActive()
    {
        return activeGravityRegion != null && (player.isPlayerGrounded() || player.isPlayerJumping());
    }

    private void SetActiveGravityRegion(GravityRegion region) {
        activeGravityRegion = region;
        if (indicator != null) indicator.GravityRegionChanged(activeGravityRegion);
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

    public void ConnectToHUD(HUD hud) {
        indicator = hud.gravityFlipIndicator;
    }

}
