using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    /* speed: changeable in Inspector, speed of the platform
     * pauseDuration: changeable in Inspector, how long platform pauses at an endpoint 
     * timer: keeps track of how long platform has been stopped at endpoint */
    public float speed = 1;
    public float pauseDuration = 0.5f;
    private float timer = 0.0f;

    /* target: empty GameObject assigned in Inspector (already done through prefab)
               movable in scene, holds an endpoint position of the platform's path */
    public GameObject target;

    /* pos_one: first endpoint of platform path
     * pos_two: second endpoint of platform path
     * targetPos: target position of platform along its current path */
    private Vector3 pos_one;
    private Vector3 pos_two;
    private Vector3 targetPos;

    /* isForward: which direction the platform is moving along its path
     *      true = from pos_one to pos_two
     *      false = from pos_two to pos_one */
    private bool isForward;


    // Start is called before the first frame update
    void Start()
    {
        pos_one = this.gameObject.transform.position;
        pos_two = target.transform.position;
        isForward = false;
    }

    // Update is called once per frame
    void Update()
    {
        float move = speed * Time.deltaTime;

        // forward/backward movement checking+swap
        if (isForward && Vector3.Distance(transform.position, pos_two) < 0.001f)
        {
            isForward = false;
            targetPos = pos_one;
            timer = 0.0f;
        }
        else if (!isForward && Vector3.Distance(transform.position, pos_one) < 0.001f)
        {
            isForward = true;
            targetPos = pos_two;
            timer = 0.0f;
        }
        else { } //changes nothing so it if messes up we know it's messed up

        // endpoint pause if/else
        if (timer >= pauseDuration)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, move);
        }
        else
        {
            timer += Time.deltaTime;
        }

    }
}
