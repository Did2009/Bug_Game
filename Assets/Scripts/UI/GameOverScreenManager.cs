using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreenManager : MonoBehaviour
{
    [Header("Game Over Variables")]
    public Image gameOverScreen;
    public GameObject playerUI;
    public GameObject tryAgainButton;
    public GameObject mainMenuButton;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set Game Over Screen
        playerUI.SetActive(true);
        gameOverScreen.enabled = false;
        tryAgainButton.SetActive(false);
        mainMenuButton.SetActive(false);
    }

    public void ShowGameOverScreen()
    {
        playerUI.SetActive(false);
        gameOverScreen.enabled = true;
        tryAgainButton.SetActive(true);
        mainMenuButton.SetActive(true);
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
