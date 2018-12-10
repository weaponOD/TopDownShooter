using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour {

    [SerializeField] private GameObject continueButton;
    [SerializeField] private TextMeshProUGUI textDisplay;
    [SerializeField] private Animator textDisplayAnim;

    /// <summary>
    /// The array of sentences that will be displayed, currently does not handle user stories, strictly hard coded sentences
    /// </summary>
    [SerializeField] private string[] sentences;
    /// <summary>
    /// The Speed at which letters will be shown
    /// </summary>
    [SerializeField] private float typingSpeed;



    private int index;


    private void Start()
    {

        StartCoroutine(Type());
    }

    private void Update()
    {
        if(textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true);
        }
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {

        textDisplayAnim.SetTrigger("change");
        continueButton.SetActive(false);

        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            continueButton.SetActive(false);
        }
    }
}
