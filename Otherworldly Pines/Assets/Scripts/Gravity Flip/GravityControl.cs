using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityControl : GravityAffected, IHUDConnected {

    [SerializeField] private GameObject sm;
    private SoundManager smScript;

    private GravityFlipIndicator indicator;
    private GravityRegion activeGravityRegion;
    private PlayerControls player;
    private bool canFlip = true;

    private bool controlsFrozen = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
        smScript = sm.GetComponent<SoundManager>();
    }

    void Update()
    {
        if (controlsFrozen) return;
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isFlipActive())
            {
                if (!activeGravityRegion.gravityIsFlipped) smScript.PlayGravflipUp();
                else smScript.PlayGravflipDown();
                activeGravityRegion.FlipGravity();
                smScript.SwapMusic();
                player.setJumping(false);
            }
            else smScript.PlayGravflipUnavailable();
        }
    }

    private bool isFlipActive()
    {
        return activeGravityRegion != null && activeGravityRegion.playerCanFlipGravity && (player.isPlayerGrounded() || player.isPlayerJumping());
    }

    private void SetActiveGravityRegion(GravityRegion region) {
        activeGravityRegion = region;
        smScript.SetActiveGravityRegion(region);
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

    public void FreezeControls() {
        controlsFrozen = true;
    }

    public void UnfreezeControls() {
        controlsFrozen = false;
    }

}
