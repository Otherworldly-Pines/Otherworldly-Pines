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
    
    private static CollectibleUIControl instance;
    private static bool apple = false;
    private static bool banana = false;
    private static bool cookie = false;
    private static bool cupcake = false;
    private static bool pie = false;
    private static bool water = false;
    
    void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() {
        Collectible1.SetActive(apple);
        Collectible2.SetActive(banana);
        Collectible3.SetActive(cookie);
        Collectible4.SetActive(cupcake);
        Collectible5.SetActive(pie);
        Collectible6.SetActive(water);
    }

    public void collect1Active()
    {
        apple = true;
        Collectible1.SetActive(true);
    }
    
    public void collect2Active()
    {
        banana = true;
        Collectible2.SetActive(true);
    }

    public void collect3Active()
    {
        cookie = true;
        Collectible3.SetActive(true);
    }

    public void collect4Active()
    {
        cupcake = true;
        Collectible4.SetActive(true);
    }

    public void collect5Active()
    {
        pie = true;
        Collectible5.SetActive(true);
    }

    public void collect6Active()
    {
        water = true;
        Collectible6.SetActive(true);
    }
}
