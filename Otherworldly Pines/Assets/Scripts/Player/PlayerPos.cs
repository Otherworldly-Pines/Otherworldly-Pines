using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPos : MonoBehaviour {
    void Start() {
        transform.position = CheckpointMaster.spawnPoint;

        var spirit = GameObject.FindObjectOfType<Spirit>();
        if (spirit != null) {
            spirit.transform.position = transform.position - new Vector3(2f, 2f, 0f);
        }
    }
}
