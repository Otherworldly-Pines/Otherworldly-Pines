using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class LevelChanger : MonoBehaviour
{
    public Animator fade_animator;
    public float transitionTime = 1f;
    public string levelName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(LoadLevel());
        }
    }

    IEnumerator LoadLevel()
    {
        fade_animator.SetBool("FadeOut", true);
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelName);
    }
}
