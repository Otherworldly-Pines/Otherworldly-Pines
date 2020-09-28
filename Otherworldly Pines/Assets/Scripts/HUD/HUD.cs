using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour {

    public HealthBar healthBar;
    public GravityFlipIndicator gravityFlipIndicator;

    private void Start() {
        GameObject player = GameObject.Find("Player");
        if (player == null) return;

        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null) playerHealth.healthBar = healthBar;

        GravityControl playerGravityControls = player.GetComponent<GravityControl>();
        if (playerGravityControls != null) playerGravityControls.indicator = gravityFlipIndicator;
    }

}
