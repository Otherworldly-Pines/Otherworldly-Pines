using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour, IHUDConnected
{
    private CollectibleUIControl collectUI;

    public GameObject sm;
    private SoundManager smScript;

    private void Start()
    {
        smScript = sm.GetComponent<SoundManager>();
    }

    void Update()
    {
    }
    public void ConnectToHUD(HUD hud)
    {
        collectUI = hud.CollectibleUIControl;
    }
    
    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.CompareTag("Collectible1"))
        {
            Destroy(target.gameObject);
            smScript.PlayCollectible();
            collectUI.collect1Active();
            EndsceneController.collect1Active();
        }
        else if (target.gameObject.CompareTag("Collectible2"))
        {
            Destroy(target.gameObject);
            smScript.PlayCollectible();
            collectUI.collect2Active();
            EndsceneController.collect2Active();
        }
        else if (target.gameObject.CompareTag("Collectible3"))
        {
            Destroy(target.gameObject);
            smScript.PlayCollectible();
            collectUI.collect3Active();
            EndsceneController.collect3Active();
        }
        else if (target.gameObject.CompareTag("Collectible4"))
        {
            Destroy(target.gameObject);
            smScript.PlayCollectible();
            collectUI.collect4Active();
            EndsceneController.collect4Active();
        }
        else if (target.gameObject.CompareTag("Collectible5"))
        {
            Destroy(target.gameObject);
            smScript.PlayCollectible();
            collectUI.collect5Active();
            EndsceneController.collect5Active();
        }
        else if (target.gameObject.CompareTag("Collectible6"))
        {
            Destroy(target.gameObject);
            smScript.PlayCollectible();
            collectUI.collect6Active();
            EndsceneController.collect6Active();
        }

    }
}
