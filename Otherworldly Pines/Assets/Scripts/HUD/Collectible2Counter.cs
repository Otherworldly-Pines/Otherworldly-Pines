using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectible2Counter : MonoBehaviour {

    public Text text;

    public void SetCount(int count) {
        text.text = count + "x";
    }
}
