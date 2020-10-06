using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SelectionBase]
public class StasisDisplay : MonoBehaviour {

    public Slider slider;
    public Image icon;

    public void SetStasisEnabled(bool enabled) {
        if (enabled) icon.color = Color.white;
        else icon.color = new Color(0.75f, 0.75f, 0.75f, 0.5f);
    }

    private void Start() {
        SetPercent(1f);
    }

    public void SetPercent(float p) {
        slider.value = p;
    }

}
