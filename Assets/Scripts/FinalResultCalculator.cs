using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class FinalResultCalculator : MonoBehaviour
{
    public static float finalScore;

    [SerializeField]
    private TextMeshProUGUI totalScoreText;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private string sceneToLoad;

    [SerializeField]
    private GameObject acknowledgmentObject;


    private void Start()
    {
        finalScore = 0f;
        TotalScore();
    }

    //use this commented method if you are adding a buttonClick before the
    //test result is displayed.
    //public void TotalScore(Button overallAnswer)
    public void TotalScore()
    {
        anim.gameObject.SetActive(true);
        anim.Play("Loading Answer");
        
        //overallAnswer.interactable = false;
        
                
        /*int scoreCalculation = ResultManager.newScore + QuizManager.score;
        finalScore = Mathf.CeilToInt(((float) scoreCalculation / 34) * 100);
        totalScoreText.text = $"{finalScore}%";*/

        StartCoroutine(DisplayAnswer()); 
        
        StartCoroutine(DisplayAcknowledgementObject(5)); 
        
        Invoke("DelayedRestart", 9);
    }

    IEnumerator DisplayAnswer()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(2, 5));
        
        anim.gameObject.SetActive(false);
        
        int scoreCalculation = ResultManager.newScore + QuizManager.score;
        finalScore = Mathf.CeilToInt(((float) scoreCalculation / 34) * 100);
        totalScoreText.text = $"{finalScore}%";
    }
    
    void DelayedRestart()
    {
        SceneManager.LoadSceneAsync(sceneToLoad);
    }
    
    IEnumerator DisplayAcknowledgementObject(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        acknowledgmentObject.SetActive(true);
    }
}
