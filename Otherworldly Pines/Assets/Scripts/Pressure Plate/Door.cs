using UnityEngine;

public class Door : MonoBehaviour, PressurePlateActivated {

    public void PPEnable() {
        gameObject.SetActive(false);
    }

    public void PPDisable() {
        gameObject.SetActive(true);
    }

}