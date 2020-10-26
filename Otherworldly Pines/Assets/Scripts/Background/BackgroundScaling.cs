using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScaling : MonoBehaviour
{

    private GameObject child;
    private ParallaxBackground pbScript;
    private float ratio; //ratio of camera to obj length

    // Start is called before the first frame update
    void Start()
    {
        child = transform.GetChild(0).gameObject;
        pbScript = child.GetComponent<ParallaxBackground>();
        ratio = pbScript.getRatio();

        transform.localScale = new Vector3(ratio * transform.localScale.x, ratio * transform.localScale.y, transform.localScale.z);
    }
}
