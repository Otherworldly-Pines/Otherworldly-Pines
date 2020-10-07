using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPos : MonoBehaviour
{
    private CheckpointMaster cm;
    // Start is called before the first frame update
    void Start()
    {
        cm = GameObject.FindGameObjectWithTag("CM").GetComponent<CheckpointMaster>();
        transform.position = cm.lastCheckPointPos;

        Spirit spirit = GameObject.FindObjectOfType<Spirit>();
        spirit.transform.position = transform.position - new Vector3(2f, 2f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
