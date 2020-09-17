using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    private int state = 0;
    public float speed;
    private bool rest = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(!this.rest){
            gameObject.transform.Translate(new Vector2(this.state * Time.deltaTime * speed, 0));
		}
       
    }

    public void moveLeft(){
        this.state = -1;
	}

    public void moveRight(){
        this.state = 1;
	}

    public void stopMoving(){
        this.state = 0;
	}

    public bool isResting(){
        return this.rest;
	}

    public IEnumerator restForSeconds(float second){
        this.rest = true;
        yield return new WaitForSeconds(second);
        Debug.Log(this.rest);
        this.rest = false;
	}
}
