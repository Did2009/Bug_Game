using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerUIManager : MonoBehaviour
{
    [Header("Game Over Variables")]
    public Image gameOverScreen; 
    public GameObject tryAgainButton;

    [Header("Collectibles' Variables")]
    public TMP_Text scoreTrackerText;
    public int score;

    private void Start()
    {
        // Set Score
        score = 0;
        scoreTrackerText.text = score.ToString();

        // Set Game Over Screen
        gameOverScreen.enabled = false;
        tryAgainButton.SetActive(false);
    }

    public void IncreaseScore()
    {
        score++;
        scoreTrackerText.text = score.ToString();
    }

    public void ShowGameOverScreen()
    {
        gameOverScreen.enabled = true;
        tryAgainButton.SetActive(true);
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(1);
    }
}
