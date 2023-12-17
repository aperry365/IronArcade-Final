using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;


public class QuizManager : MonoBehaviour { 

    public bool IsBasketball = false;
    public bool IsBowling = false;
    public bool IsArchery = false;
    public bool IsRocket = false;
    
    
    public Question[] questions; // Array to hold Q's
    public Text questionText; // Reference to the UI text displaying the question
    public Button[] answerButtons; // Array of buttons for answers


    private int currentQuestionIndex = 0; // Index tracks current question
    private int currentRound = 0; // Variable tracks current round
    

    public Shooter shooterScript;
    public Striker strikerScript;
    public BowLogic bowLogicScript;
    public Movement1 RocketScript;


    public Text QuestionResult;
    public Text ScoreResult;
    public GameObject QuizBoard;

    public int CurrentScore;

    bool IsShooting = false;


private void Start() {

    // Load last saved question index or start from the first question if no index is saved
    currentQuestionIndex = PlayerPrefs.GetInt("CurrentQuestionIndex", 0);

    // Load the last saved round index or start from the first round if no index is saved
    currentRound = PlayerPrefs.GetInt("CurrentRoundIndex", 0);

    CurrentScore = PlayerPrefs.GetInt("Score", 0);
    ShowQuestion(currentQuestionIndex); // Show the first question when the game starts
}



private void Update() {
    ScoreResult.text = "Score: " + CurrentScore.ToString();
}



// Method to display a question and its answers
private void ShowQuestion(int index)
{
    if (index < questions.Length)
    {
        questionText.text = questions[index].question; // Display the question

        // Display the answers on the buttons
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<Text>().text = questions[index].answers[i];
        }
    }
    else
    {
        Debug.Log("Quiz complete!"); // If all questions have been answered
                                      // Logic for quiz complete

        // Reset the saved question index when the quiz completes
        PlayerPrefs.DeleteKey("CurrentQuestionIndex");
        PlayerPrefs.Save();

        // Restart the quiz by resetting the currentQuestionIndex to 0
        currentQuestionIndex = 0;
        ShowQuestion(currentQuestionIndex); // Show the first question again
        return; // Exit the method to stop executing
    }

    // Save the current question index
    PlayerPrefs.SetInt("CurrentQuestionIndex", currentQuestionIndex);
    PlayerPrefs.Save();
}

// Method called when an answer button is clicked
public void AnswerButtonClicked(int buttonIndex)
{
    if (buttonIndex == questions[currentQuestionIndex].correctAnswerIndex)
    {
        Debug.Log("Correct!"); // Logic for correct answer
        QuestionResult.text = "Correct! +10";
        ScoreIncrease(10);

        if (IsBasketball)
        {
            shooterScript.IsCorrect = true;
        }
        if (IsBowling)
        {
            strikerScript.IsCorrect = true;
        }
        if (IsArchery)
        {
            bowLogicScript.IsCorrect = true;
        }
        if (IsRocket)
        {
            RocketScript.IsCorrect = true;
        }

        StartCoroutine(ShootAfterDelay(2f));
    }
    
    else
    {
        QuestionResult.text = "Incorrect!";
        Debug.Log("Incorrect!"); // Logic for incorrect answer

        if (IsBasketball)
        {
            shooterScript.IsCorrect = false;
        }
        if (IsBowling)
        {
            strikerScript.IsCorrect = false;
        }
        if (IsArchery)
        {
            bowLogicScript.IsCorrect = false;
        }
        if (IsRocket)
        {
            RocketScript.IsCorrect = false;
        }

        StartCoroutine(ShootAfterDelay(1f));
    }

    currentQuestionIndex++; // Move to next question
    currentRound++;
    
    // Save the current question index
    PlayerPrefs.SetInt("CurrentRoundIndex", currentRound);
    PlayerPrefs.Save();
    print(currentRound);
    ShowQuestion(currentQuestionIndex); // Display the next question

    if (currentRound == 4)
    {
        currentQuestionIndex++; // Move to next question
        currentRound = 0;
        FinishRound();
        
        // Save the current question index
        PlayerPrefs.SetInt("CurrentQuestionIndex", currentQuestionIndex);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("CurrentRoundIndex", currentRound);
        PlayerPrefs.Save();
    }
}

private void FinishRound()
{
    Debug.Log("Round complete!");

    
    
}

IEnumerator ShootAfterDelay(float delay)
{
    QuizBoard.SetActive(false);

    yield return new WaitForSeconds(delay);
    if (IsBasketball)
    {
        if (!IsShooting)
        {
            IsShooting = true;
            shooterScript.Shoot();
        }
    }
    if (IsBowling)
    {
        if (!IsShooting)
        {
            IsShooting = true;
            strikerScript.Shoot();
        }
    }
    if (IsArchery)
    {
        IsShooting = true;
        bowLogicScript.Shoot();
    }

    if (IsRocket)
    {
        RocketScript.ActivateBooster();
    }

    yield return new WaitForSeconds(0.1f);
    QuestionResult.text = "";

    IsShooting = false;

    yield return new WaitForSeconds(3f);
    QuizBoard.SetActive(true);
    if (currentRound == 0)
    {
        QuizBoard.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
}

public void ScoreIncrease(int Amount) {
    CurrentScore += Amount;
    PlayerPrefs.SetInt("Score", CurrentScore);
    PlayerPrefs.Save();
    }
}

[Serializable]
public class Question
{
    public string question; 
    public string[] answers = new string[4]; // Array to hold answers
    public int correctAnswerIndex; // Index of correct answer
}

