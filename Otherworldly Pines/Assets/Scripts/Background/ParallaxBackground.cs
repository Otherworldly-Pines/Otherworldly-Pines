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

    public bool isLeft; //if is initially left obj or not

    private float move; // parallax movement value
    private Vector3 startPos;
    private float objLength, objHeight;
    private float screenHeightInUnits, screenWidthInUnits;

    private float awakeHeight, awakeWidth;

    private float ratio;

    // Start is called before the first frame update
    void Awake()
    {

        awakeHeight = Camera.main.orthographicSize * 2;
        awakeWidth = awakeHeight * Screen.width / Screen.height;

        ratio = (awakeWidth)/ GetComponent<SpriteRenderer>().bounds.size.x;

        //transform.localScale = new Vector3(ratio * transform.localScale.x, ratio * transform.localScale.y, transform.localScale.z);
        if (isLeft)
            transform.position = new Vector3(-(ratio * GetComponent<SpriteRenderer>().bounds.size.x) / 2, transform.position.y + offsetY, transform.position.z);
        else
            transform.position = new Vector3((ratio * GetComponent<SpriteRenderer>().bounds.size.x) / 2, transform.position.y + offsetY, transform.position.z);
            
        startPos = transform.position;
        objLength = ratio * GetComponent<SpriteRenderer>().bounds.size.x;
        objHeight = ratio * GetComponent<SpriteRenderer>().bounds.size.y;
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        screenHeightInUnits = Camera.main.orthographicSize * 2;
        screenWidthInUnits = screenHeightInUnits * Screen.width / Screen.height;

        move = cam.transform.position.x * bgMoveSpeed;
        
        var unscrolledPos = new Vector3(startPos.x + move, cam.transform.position.y + offsetY, transform.position.z);

        // if object is too far left or right (is offscreen), moves it back over
        // creates the "scrolling background"
        if (unscrolledPos.x - cam.transform.position.x < -1.0 * screenWidthInUnits) {
            transform.position = new Vector3(unscrolledPos.x + (2 * objLength), unscrolledPos.y, unscrolledPos.z);
        } else if (unscrolledPos.x - cam.transform.position.x > 1.0 * screenWidthInUnits) {
            transform.position = new Vector3(unscrolledPos.x - (2 * objLength), unscrolledPos.y, unscrolledPos.z);
        } else {
            transform.position = unscrolledPos;
        }
    }

    public float getRatio()
    {
        return ratio;
    }
}
