using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    public GameObject dialogDisplay;
    public string[] sentences;
  
    public float charTypingInterval;

    public GameObject continueButton;
    private int sentenceIndex;
    void Start()
    {
        StartCoroutine(TypeText());
    }

    private void Update()
    {
        displayDialog();
        //display the continue button when the sentence is finished
        if(dialogText.text == sentences[sentenceIndex])
        {
            continueButton.SetActive(true);
        }
        
    }

    IEnumerator TypeText()
    {
        foreach(char letter in sentences[sentenceIndex].ToCharArray()){
            dialogText.text += letter;
            yield return new WaitForSeconds(charTypingInterval);
        }
    }

    public void NextSentence()
    {
        //hide the continue button before the sentence is finished
        continueButton.SetActive(false);
        if(sentenceIndex < sentences.Length - 1)
        {
            sentenceIndex++;
            dialogText.text = "";
            StartCoroutine(TypeText());
        }
        else
        {
            //reset the sentence sequence
            dialogText.text = "";
            dialogDisplay.SetActive(false);
            sentenceIndex = 0;
            StartCoroutine(TypeText());
        }
    }

    void displayDialog()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            dialogDisplay.SetActive(true);
    }
}
