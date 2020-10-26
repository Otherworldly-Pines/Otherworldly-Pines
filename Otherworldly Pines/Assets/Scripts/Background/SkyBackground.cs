using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBackground : MonoBehaviour
{
    // camera object, assigned in inspector
    [SerializeField] GameObject cam;

    private SpriteRenderer spriteRenderer;
    private float camWidth, camHeight;
    private Vector2 spriteSize;

    // Start is called before the first frame update
    // ideally we are not resizing the screen during gameplay or it starts at fullscreen (max size)
    // scales the sky background image to fit the width of the camera while maintaining proportion
    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        camHeight = Camera.main.orthographicSize * 2;
        camWidth = camHeight * Camera.main.aspect;
        spriteSize = spriteRenderer.sprite.bounds.size;

        Vector2 bgScale = transform.localScale;

        bgScale *= camHeight / spriteSize.y;
        transform.localScale = bgScale;

    }
}
