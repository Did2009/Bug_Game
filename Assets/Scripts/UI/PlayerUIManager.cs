using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerUIManager : MonoBehaviour
{
    // Singleton
    public static PlayerUIManager instance;

    // References
    CharacterManager characterManager;
    GameOverScreenManager gameOverScreenManager;
    VictoryScreenManager victoryScreenManager;
    MoveToNextScene moveToNextScene;

    [Header("For Switching Levels:")]
    public int savedSelectedCharacter;

    [Header("Health Variables")]
    public Image livesImage;
    public Animator livesAnimator;
    public int currentHP = 3;

    [Header("Collectibles' Variables")]
    public TMP_Text scoreTrackerText;
    public int score;

    private void Awake()
    {
        // There can only be one instance of this script at a time
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        // Set starting score
        score = 0;
        // Update the score UI
        scoreTrackerText.text = score.ToString();

        // Subscribe to OnSceneChange event
        SceneManager.activeSceneChanged += OnSceneChange;
    }

    private void OnSceneChange(Scene oldScene, Scene newScene)
    {
        // Find the Chracter Manager
        characterManager = FindAnyObjectByType<CharacterManager>();
        // Set this instance to it's variable
        characterManager.playerUIManager = this;

        switch (newScene.buildIndex)
        {
            // The new scene is the Main Menu
            case 0:
                // Turn off UI
                this.gameObject.SetActive(false);
                break;

            // The new scene is the Level (also for restarting)
            case 1:
                // Turn on UI
                this.gameObject.SetActive(true);

                // Find the Game Over Screen Manager
                gameOverScreenManager = FindAnyObjectByType<GameOverScreenManager>();
                // Set this instance to it's variable
                gameOverScreenManager.playerUI = this.gameObject;

                // Find the Move To Next Scene Script
                moveToNextScene = FindAnyObjectByType<MoveToNextScene>();
                // Set this instance to it's variable
                moveToNextScene.playerUIManager = this;

                // Reset score
                score = 0;
                // Update score tracker
                scoreTrackerText.text = score.ToString();
                // Reset lives
                currentHP = 3;
                // Update health tracker
                UpdateHealth();
                break;

            // The new scene is the Window
            case 2:
                // Turn on UI
                this.gameObject.SetActive(true);

                // Find the Victory Screen
                victoryScreenManager = FindAnyObjectByType<VictoryScreenManager>();
                // Set this instance to it's variable
                victoryScreenManager.playerUI = this.gameObject;
                break;

            // The new scene is another result (testground)
            default:
                // Turn on UI
                this.gameObject.SetActive(true);
                break;
        }
    }

    public void IncreaseScore()
    {
        score++;
        scoreTrackerText.text = score.ToString();
    }

    public void UpdateHealth()
    {
        switch (currentHP)
        {
            case 3:
                livesAnimator.Play("3");
                break;
            case 2:
                livesAnimator.Play("2");
                StartCoroutine(FlashRed());
                break;
            case 1:
                livesAnimator.Play("1");
                StartCoroutine(FlashRed());
                break;
            case 0:
                livesAnimator.Play("0");
                StartCoroutine(FlashRed());
                break;
            default:
                Debug.Log("Lives Error");
                break;
        }
    }

    IEnumerator FlashRed()
    {
        livesImage.color = Color.red;

        yield return new WaitForSeconds(0.15f);

        livesImage.color = Color.white;

        yield return null;
    }
}
