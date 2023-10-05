using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


//[assembly:System.Runtime.CompilerServices.InternalsVisibleTo("Assembly-CSharp-Editor")]

public class QuizManager : MonoBehaviour
{

    [SerializeField] private GameObject submitButton;

    [SerializeField] private GameObject immediateResult;

    [SerializeField] private Color initialColour;

    [SerializeField] private TextMeshProUGUI counterText;

    [SerializeField] private TextMeshProUGUI warningtext;

    [SerializeField] private TextMeshProUGUI scoreText;

    public static int score;

    [SerializeField] private ResultManager resultManager;

    //[SerializeField]
    //private TextMeshProUGUI finalScoreText;
    //private static int finalScore = 0;
    //private static int generatedScore = 0;


    [SerializeField] private int numberOfOptions;

    [SerializeField] private GameObject nextVideo;

    [SerializeField] private List<int> correctOptions = new List<int>();

    private List<int> chosenOptions = new List<int>();

    //[SerializeField]
    //public TextMeshProUGUI finalScoreText;

    private List<Option> options;

    // Start is called before the first frame update
    void Start()
    {
        options = GetComponentsInChildren<Option>().ToList();
        initialColour = Color.white;
    }

    public void SelectedOptions(Option option)
    {
        option.optionImage.color = new Color(0.3163937f, 0.6443403f, 0.8490566f, 1f);

        if (option.selected)
        {
            option.optionImage.color = initialColour;
            int index = chosenOptions.FindIndex((i => i == option.optionNumber));
            chosenOptions.RemoveAt(index);
            counterText.text = $"You have selected {chosenOptions.Count} out of {numberOfOptions} options.";
            option.selected = false;
            warningtext.gameObject.SetActive(false);
            counterText.gameObject.SetActive(true);
            return;
        }

        if (chosenOptions.Count == numberOfOptions - 1)
        {
            submitButton.GetComponent<Button>().interactable = true;
        }

        if (chosenOptions.Count < numberOfOptions)
        {
            option.optionImage.color = new Color(0.3163937f, 0.6443403f, 0.8490566f, 1f);
            chosenOptions.Add(option.optionNumber);
            option.selected = true;
            counterText.text = $"You have selected {chosenOptions.Count} out of {numberOfOptions} options.";
        }
        else
        {
            option.selected = false;
            option.optionImage.color = initialColour;
            counterText.gameObject.SetActive(false);
            warningtext.gameObject.SetActive(true);
        }
    }

    public void SubmitAnswers()
    {
        int numberOfCorrectAnswers = 0;

        for (int i = 0; i < chosenOptions.Count; i++)
        {
            if (correctOptions.Contains(chosenOptions[i]))
            {
                numberOfCorrectAnswers++;
            }
        }

        if (numberOfCorrectAnswers == numberOfOptions)
        {
            Debug.Log("I got it all!");
            scoreText.text = $"You got {numberOfCorrectAnswers} out of {numberOfOptions} option.";
            immediateResult.SetActive(true);
           // nextVideo.SetActive(true);
            submitButton.SetActive(false);
            warningtext.gameObject.SetActive(false);
            counterText.gameObject.SetActive(false);
            
            for (int j = 0; j < correctOptions.Count; j++)
            {
                options[correctOptions[j]].optionImage.color = new Color(0.1254902f, 0.9607843f, 0.7098039f, 1f);
                /*if (options[j].optionImage.color != new Color(0.1241f, 0.9607f, 0.7098f))
                {
                    options[j].gameObject.SetActive(false);
                }*/
                if (!correctOptions.Contains(options[j].optionNumber))
                {
                    options[j].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            scoreText.text = $"You got {numberOfCorrectAnswers} out of {numberOfOptions} options.";
            immediateResult.SetActive(true);
            submitButton.SetActive(false);
            warningtext.gameObject.SetActive(false);
            counterText.gameObject.SetActive(false);
            Debug.Log($"I got only {numberOfCorrectAnswers} out of {numberOfOptions}");

            List<Option> wrongOptions = new List<Option>();
            for (int i = 0; i < options.Count; i++)
            {
                if (!correctOptions.Contains(options[i].optionNumber))// && chosenOptions.Contains(chosenOptions[numberOfCorrectAnswers]))
                {
                    wrongOptions.Add(options[i]);
                    options[i].optionImage.color = new Color(1f, 0.2941f, 0.3372f);
                    //options[correctOptions[i]].optionImage.color = new Color(0.1254902f, 0.9607843f, 0.7098039f, 1f);

                    StartCoroutine(DisplayCorrectOptions());
                }
            }
        }

        //score += numberOfCorrectAnswers;
        score += numberOfCorrectAnswers;
        if (resultManager != null)
        {
            resultManager.AddScore(numberOfCorrectAnswers);
            Debug.Log("score = " + score);
        }
        //FinalResultCalculator.generatedScore += numberOfCorrectAnswers;
        //FinalResultCalculator.finalScore = Mathf.CeilToInt(((float)FinalResultCalculator.generatedScore / 34) * 100);
        //finalScoreText.text = FinalResultCalculator.finalScore+"%".ToString();

        //Debug.Log("Current score = " + FinalResultCalculator.finalScore + "%");
        Debug.Log("score = " + score);


    }

    IEnumerator DisplayCorrectOptions()
    {
        yield return new WaitForSeconds(3);

        for (int j = 0; j < correctOptions.Count; j++)
        {
            options[correctOptions[j]].optionImage.color = new Color(0.1254902f, 0.9607843f, 0.7098039f,1f);
        }

        for (int i = 0; i < options.Count; i++)
        {
            if (!correctOptions.Contains(options[i].optionNumber))
            {
                options[i].gameObject.SetActive(false);
            }
        }
        nextVideo.SetActive(true);
    }
}
