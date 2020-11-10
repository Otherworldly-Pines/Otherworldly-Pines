using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour, IHUDConnected
{
    private Collectible1Counter c1Counter;
    private Collectible2Counter c2Counter;
    public int c1 = 2;
    public int c2 = 1;
    private int c1Collected = 0; // num of C1 collected
    private int c2Collected = 0; // num of C2 collected
    private LinkedList<Collectable.Item> collectedItems;

    public GameObject sm;
    private SoundManager smScript;

    private void Start()
    {
        smScript = sm.GetComponent<SoundManager>();
    }

    void Update()
    {
        if (c1 == c1Collected)
        {
            // show all C1 are collected
        }

        if (c2 == c2Collected)
        {
            // show all C2 are collected
        }
    }
    public void ConnectToHUD(HUD hud)
    {
        c1Counter = hud.c1Counter;
        c2Counter = hud.c2Counter;
    }
    
    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Collectible1"))
        {
            Collectable collectable = target.gameObject.GetComponent<Collectable>();
            switch (collectable.getItem())
            {
                case Collectable.Item.Apple:
                //Show Apple Item
                case Collectable.Item.Banana:
                //Show Banana Item
                case Collectable.Item.Cookie:
                //Show Banana Item
                case Collectable.Item.Cupcake:
                //Show Banana Item
                case Collectable.Item.Pie:
                //Show Banana Item
                case Collectable.Item.Water:
                //Show Banana Item

                default:
                {
                    Destroy(target.gameObject);
                    c1Collected++;
                    smScript.PlayCollectible();
                    // show c1Collected/c1 are collected
                    c1Counter.SetCount(c1Collected);
                    break;
                }
                
            }
            
        }
        else if (target.gameObject.CompareTag("Collectible2"))
        {
            Destroy(target.gameObject);
            c2Collected++;
            smScript.PlayCollectible();
            // show c2Collected/c2 are collected
            c2Counter.SetCount(c2Collected);
        }

    }
}
