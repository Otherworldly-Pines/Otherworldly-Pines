using System;
using UnityEngine;

public class BerryBush : MonoBehaviour {

    [SerializeField] private Sprite spritePicked;
    private SpriteRenderer spriteRender;
    private AudioSource audioSrc;

    public int numBerries = 3;

    private void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        audioSrc = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (numBerries <= 0) return;
        
        var throwControls = other.gameObject.GetComponent<PlayerThrow>();

        if (throwControls != null) {
            throwControls.Collect(numBerries);
            numBerries = 0;

            spriteRender.sprite = spritePicked; //changes sprite to empty bush
            audioSrc.Play();
            // TODO: just change sprite or something instead
            //gameObject.SetActive(false);
        }
    }

}
