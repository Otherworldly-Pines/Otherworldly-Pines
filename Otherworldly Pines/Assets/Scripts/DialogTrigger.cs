using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogTrigger : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    public GameObject dialogDisplay;
    public GameObject continueText;
    public string[] sentences;
  
    public float charTypingInterval;

    private int sentenceIndex;

    private bool isDisplaying = false;
    private GameObject player;

    private void OnTriggerEnter2D(Collider2D other) {
        var playerControls = other.gameObject.GetComponent<PlayerControls>();
        if (playerControls != null) {
            player = other.gameObject;
            
            isDisplaying = true;

            var gravityControls = player.GetComponent<GravityControl>();
            var throwControls = player.GetComponent<PlayerThrow>();
            playerControls.FreezeControls();
            gravityControls.FreezeControls();
            throwControls.FreezeControls();

            dialogDisplay.SetActive(true);
            continueText.SetActive(false);
            dialogText.text = "";
            StartCoroutine(TypeText());
        }
    }

    private void Update() {
        if (isDisplaying) {
            if (dialogText.text == sentences[sentenceIndex] && Input.anyKeyDown) {
                if (sentenceIndex < sentences.Length - 1) {
                    NextSentence();
                } else {
                    dialogDisplay.SetActive(false);
                    
                    var playerControls = player.GetComponent<PlayerControls>();
                    var gravityControls = player.GetComponent<GravityControl>();
                    var throwControls = player.GetComponent<PlayerThrow>();
                    
                    playerControls.UnfreezeControls();
                    gravityControls.UnfreezeControls();
                    throwControls.UnfreezeControls();

                    gameObject.SetActive(false);
                }
            }
        }
    }

    IEnumerator TypeText()
    {
        foreach(char letter in sentences[sentenceIndex].ToCharArray()){
            dialogText.text += letter;
            yield return new WaitForSeconds(charTypingInterval);
        }

        continueText.SetActive(true);
    }

    public void NextSentence() {
        sentenceIndex++;
        dialogText.text = "";
        StartCoroutine(TypeText());
        continueText.SetActive(false);
    }
}
