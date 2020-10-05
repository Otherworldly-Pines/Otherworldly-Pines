using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ParallaxBackground : MonoBehaviour
{

    // camera object assigned in inspector
    [SerializeField] private GameObject cam;

    // bg movement ratio, value 0-1, assigned in inspector
    // 0 = none; 1 = foreground (following player movespd), usually too fast
    [SerializeField] private float bgMoveSpeed;

    // placing obj offsetY distance above center of cam
    [SerializeField] private float offsetY;

    private float move; // parallax movement value
    private Vector2 startPos;
    private float objLength, objHeight;
    private float screenHeightInUnits, screenWidthInUnits;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        objLength = GetComponent<SpriteRenderer>().bounds.size.x;
        objHeight = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {

        screenHeightInUnits = Camera.main.orthographicSize * 2;
        screenWidthInUnits = screenHeightInUnits * Screen.width / Screen.height;

        move = cam.transform.position.x * bgMoveSpeed;

        // moves obj
        transform.position = new Vector2(startPos.x + move, cam.transform.position.y + offsetY);

        // if object is too far left or right (is offscreen), moves it back over
        // creates the "scrolling background"
        if (transform.position.x - cam.transform.position.x < -1.5 * screenWidthInUnits)
        {
            transform.position = new Vector2(transform.position.x + (2 * objLength), transform.position.y);
        }
        else if (transform.position.x - cam.transform.position.x > 1.5 * screenWidthInUnits)
        {
            transform.position = new Vector2(transform.position.x - (2 * objLength), transform.position.y);
        }
    }
}
