using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHUDConnected {

    [SerializeField] private GameObject sm;
    private SoundManager smScript;
    public Animator animator;
    private static float maxHealth = 100f;
    private static float minHealth = 0f;
    
    private HealthBar healthBar;
    [HideInInspector] public float currentHealth = 100f;

    private void Start() {
        RefillMaxHealth();
        smScript = sm.GetComponent<SoundManager>();
    }

    private void SetHealth(float health) {
        currentHealth = health;
        if (healthBar != null) healthBar.SetHealth(currentHealth);
    }

    public void RefillMaxHealth() {
        SetHealth(maxHealth);
    }

    public void TakeDamage(float damage) {
        SetHealth(currentHealth - damage);
        if (currentHealth == minHealth) smScript.PlayDead();
        else {
            animator.SetTrigger("TriggerDamage");
            smScript.PlayHurt();
        }
    }

    public void ConnectToHUD(HUD hud) {
        healthBar = hud.healthBar;
    }

}
