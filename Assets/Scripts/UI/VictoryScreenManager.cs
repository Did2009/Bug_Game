using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryScreenManager : MonoBehaviour
{
    [Header("Victory Screen Variables")]
    public Image victoryScreenImage;
    public GameObject playerUI;
    public GameObject welcomeHomeText;
    public GameObject mainMenuButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        victoryScreenImage.enabled = false;
        welcomeHomeText.SetActive(false);
        mainMenuButton.SetActive(false);
    }

    public void ShowVictoryScreen()
    {
        playerUI.SetActive(false);
        victoryScreenImage.enabled = true;
        welcomeHomeText.SetActive(true);
        mainMenuButton.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
