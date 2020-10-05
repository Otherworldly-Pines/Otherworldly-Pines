using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SelectionBase]
public class StasisDisplay : MonoBehaviour {

    public Slider slider;

    private void Start() {
        SetPercent(1f);
    }

    public void SetPercent(float p) {
        slider.value = p;
    }

}
