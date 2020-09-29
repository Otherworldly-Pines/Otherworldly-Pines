using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SelectionBase]
public class HealthBar : MonoBehaviour {

    public Gradient gradient;
    public Image fill;
    public Slider slider;

    private void Start() {
        SetHealth(100f);
    }

    public void SetHealth(float health) {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    
}
