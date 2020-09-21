using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBehavior))]
public class RestPattern : MonoBehaviour
{
    public float restTime = 3;
    public float activeTime = 10;
    private EnemyBehavior behavior;

    // Start is called before the first frame update
    void Start()
    {
        this.behavior = gameObject.GetComponent<EnemyBehavior>();
        StartCoroutine(this.activeForSecondAndRest());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator activeForSecondAndRest(){
        yield return new WaitForSeconds(this.activeTime);
        yield return StartCoroutine(this.behavior.restForSeconds(this.restTime));
        StartCoroutine(this.activeForSecondAndRest());
	}


}
