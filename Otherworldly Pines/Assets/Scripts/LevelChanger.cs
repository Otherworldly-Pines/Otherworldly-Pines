using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class LevelChanger : MonoBehaviour
{
    public Animator fade_animator;
    public float transitionTime = 1f;
    public string levelName;

    void onTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            StartCoroutine(LoadLevel());
        }
    }

    IEnumerator LoadLevel()
    {
        fade_animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelName);
    }
}
