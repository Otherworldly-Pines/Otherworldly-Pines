using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BasicMovement))]
public class RestPattern : MonoBehaviour
{
    public float restTime = 3;
    public float activeTime = 10;
    private BasicMovement movement;

    // Start is called before the first frame update
    void Start()
    {
        this.movement = gameObject.GetComponent<BasicMovement>();
        StartCoroutine(this.activeForSecondAndRest());
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    IEnumerator activeForSecondAndRest(){
        yield return new WaitForSeconds(this.activeTime);
        yield return StartCoroutine(this.movement.restForSeconds(this.restTime));
        StartCoroutine(this.activeForSecondAndRest());
	}


}
