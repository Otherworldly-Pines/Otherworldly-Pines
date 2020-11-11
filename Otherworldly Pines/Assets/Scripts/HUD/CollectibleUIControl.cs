using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleUIControl : MonoBehaviour {

    public GameObject Collectible1;
    public GameObject Collectible2;
    public GameObject Collectible3;
    public GameObject Collectible4;
    public GameObject Collectible5;
    public GameObject Collectible6;

    private void Start() {
        Collectible1.SetActive(false);
        Collectible2.SetActive(false);
        Collectible3.SetActive(false);
        Collectible4.SetActive(false);
        Collectible5.SetActive(false);
        Collectible6.SetActive(false);
    }

    public void collect1Active() 
    {
        Collectible1.SetActive(true);
    }
    
    public void collect2Active() 
    {
        Collectible2.SetActive(true);
    }

    public void collect3Active() 
    {
        Collectible3.SetActive(true);
    }

    public void collect4Active() 
    {
        Collectible4.SetActive(true);
    }

    public void collect5Active() 
    {
        Collectible5.SetActive(true);
    }

    public void collect6Active() 
    {
        Collectible6.SetActive(true);
    }
}
