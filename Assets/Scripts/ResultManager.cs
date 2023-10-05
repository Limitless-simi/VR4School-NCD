using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;



//[assembly:System.Runtime.CompilerServices.InternalsVisibleTo("Assembly-CSharp-Editor")]

public class ResultManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] questions;

   [SerializeField]
    private GameObject youAreCorrect;

    [SerializeField]
    private GameObject youAreWrong;

    [SerializeField]
    private GameObject correctAnswer;

    [SerializeField]
    private GameObject correctAnswer2;

    private int currentOption;

    public static int newScore;

    public TMP_Text scoreText;


    public void CorrectAnswer(Button correctOption)
    {
        correctOption.GetComponent<Image>().color = new Color(0.1254902f, 0.9607843f, 0.7098039f, 1f);
        
        Invoke("DisplayCorrectOptionBadge", 2);
        
        newScore++;
        Debug.Log("Newscore = " + newScore);

        UpdateScoreText(); // Added this line to update the score text

        StartCoroutine(DisableBoard1());
    }

    void UpdateScoreText()
    {
        scoreText.text = " " + newScore.ToString() + "/2";
    }

    public void OnNextQuestionClicked()
    {
        correctAnswer.SetActive(false);
        currentOption++;
        for (int i = 0; i < questions.Length; i++)
        {
            questions[i].SetActive(false);
            questions[currentOption].SetActive(true);
        }
    }

    public void WrongAnswer(Button thisButton)
    {
        thisButton.GetComponent<Image>().color = new Color(1f, 0.2941f, 0.3372f, 1f);

     //   Invoke("DisplayWrongOptionBadge", 2);

        StartCoroutine(DisableBoard());
    }

    IEnumerator DisableBoard()
    {
        yield return new WaitForSeconds(3);
        youAreCorrect.SetActive(false);
        
        correctAnswer.SetActive(true);
        
        for (int i = 0; i < questions.Length; i++)
        {
            questions[i].SetActive(false);
        }
    }

    IEnumerator DisableBoard1()
    {
        yield return new WaitForSeconds(3);
        youAreCorrect.SetActive(false);

        //correctAnswer.SetActive(true);

        for (int i = 0; i < questions.Length; i++)
        {
            questions[i].SetActive(false);
        }

        OnNextQuestionClicked();
    }

    public void OptionClickedOnQuestion2(Button clickedOption)
    {
        // Assuming option is wrong by default
        bool isCorrect = false;

        // Logic to determine if the clicked option is correct
        // Example: if (clickedOption.name == "CorrectOptionName")
        //     isCorrect = true;

        if (!isCorrect)
        {
            clickedOption.GetComponent<Image>().color = new Color(1f, 0.2941f, 0.3372f, 1f); // Set the option color to red
            StartCoroutine(ShowCorrectAnswer2AfterDelay());
        }
    }

    IEnumerator ShowCorrectAnswer2AfterDelay()
    {
        yield return new WaitForSeconds(2);
        correctAnswer2.SetActive(true);
        for (int i = 0; i < questions.Length; i++)
        {
            questions[i].SetActive(false);
        }
    }


    public void AddScore(int addedScore)
    {
        newScore += addedScore;
        UpdateScoreText();
    }

    public void GoToTheNextVideo(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    void DisplayCorrectOptionBadge()
    {
        youAreCorrect.SetActive(true);
        
        for (int i = 0; i < questions.Length; i++)
        {
            questions[i].SetActive(false);
        }
    }

   /* void DisplayWrongOptionBadge()
    {
        youAreWrong.SetActive(true);

        for (int i = 0; i < questions.Length; i++)
        {
            questions[i].SetActive(false);
        }
    }*/
}
