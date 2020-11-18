using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndsceneController : MonoBehaviour
{
    public GameObject Collectible1;
    public GameObject Collectible2;
    public GameObject Collectible3;
    public GameObject Collectible4;
    public GameObject Collectible5;
    public GameObject Collectible6;

    private static bool apple = false;
    private static bool banana = false;
    private static bool cookie = false;
    private static bool cupcake = false;
    private static bool pie = false;
    private static bool water = false;

    void Start()
    {
        Collectible1.SetActive(apple);
        Collectible2.SetActive(banana);
        Collectible3.SetActive(cookie);
        Collectible4.SetActive(cupcake);
        Collectible5.SetActive(pie);
        Collectible6.SetActive(water);
    }

    public static void ResetCollectables() {
        apple = false;
        banana = false;
        cookie = false;
        cupcake = false;
        pie = false;
        water = false;
    }

    public static void collect1Active()
    {
        apple = true;
    }
    
    public static void collect2Active()
    {
        banana = true;
    }

    public static void collect3Active()
    {
        cookie = true;
    }

    public static void collect4Active()
    {
        cupcake = true;
    }

    public static void collect5Active()
    {
        pie = true;
    }

    public static void collect6Active()
    {
        water = true;
    }
}
