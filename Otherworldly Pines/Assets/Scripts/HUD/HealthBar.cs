using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SelectionBase]
public class HealthBar : MonoBehaviour {

    public Slider slider;

    private void Start() {
        SetHealth(100f);
    }

    public void SetHealth(float health) {
        slider.value = health;
    }
    
}
