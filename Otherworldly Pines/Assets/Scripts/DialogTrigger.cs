using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogTrigger : MonoBehaviour
{
    public DialogBox dialogDisplay;
    public string[] sentences;
    public bool skippable = true;
  
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
            
            PauseMenu.IsDisabledByTutorialText = true;
            
            dialogDisplay.gameObject.SetActive(true);
            dialogDisplay.skipObject.SetActive(false);
            dialogDisplay.skipText.text = skippable ? "Press esc to skip…" : "Cannot skip";
            dialogDisplay.continueText.SetActive(false);
            dialogDisplay.mainText.text = "";
            StartCoroutine(TypeText());
        }
    }

    private void Update() {
        if (isDisplaying) {
            if (Input.GetKeyUp(KeyCode.Escape)) {
                if (skippable) Close();
                else {
                    dialogDisplay.skipObject.SetActive(true);
                }
            } else if (dialogDisplay.mainText.text == sentences[sentenceIndex] && Input.anyKeyDown) {
                if (sentenceIndex < sentences.Length - 1) {
                    NextSentence();
                } else {
                    Close();
                }
            }
        }
    }

    IEnumerator TypeText()
    {
        foreach(char letter in sentences[sentenceIndex].ToCharArray()){
            dialogDisplay.mainText.text += letter;
            yield return new WaitForSeconds(charTypingInterval);
        }

        if (skippable) {
            dialogDisplay.skipObject.SetActive(true);
        } else {
            dialogDisplay.skipText.text = "Cannot skip";
        }

        dialogDisplay.continueText.SetActive(true);
    }

    public void NextSentence() {
        sentenceIndex++;
        dialogDisplay.mainText.text = "";
        StartCoroutine(TypeText());
        dialogDisplay.continueText.SetActive(false);
    }

    private void Close() {
        dialogDisplay.gameObject.SetActive(false);
                    
        var playerControls = player.GetComponent<PlayerControls>();
        var gravityControls = player.GetComponent<GravityControl>();
        var throwControls = player.GetComponent<PlayerThrow>();
                    
        playerControls.UnfreezeControls();
        gravityControls.UnfreezeControls();
        throwControls.UnfreezeControls();

        gameObject.SetActive(false);

        PauseMenu.IsDisabledByTutorialText = false;
    }
}
