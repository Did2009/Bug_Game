using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerUIManager : MonoBehaviour
{
    DataManager dataManager;

    [Header("Health Variables")]
    public Image livesImage;
    public Animator livesAnimator;
    public int currentHP = 3;

    [Header("Collectibles' Variables")]
    public TMP_Text scoreTrackerText;
    public int score;

    private void Start()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            // The new scene is the Level (also for restarting)
            case 1:
                // Turn on UI
                this.gameObject.SetActive(true);

                // Reset score
                score = 0;
                // Update score tracker
                scoreTrackerText.text = score.ToString();
                // Reset health
                currentHP = 3;
                // Update health tracker
                UpdateHealth();
                break;

            // The new scene is the Window
            case 2:
                // Turn on UI
                this.gameObject.SetActive(true);

                // Find the Data Manager
                dataManager = FindAnyObjectByType<DataManager>();

                // Set score and HP to saved data
                score = dataManager.savedScore;
                currentHP = dataManager.savedHP;

                // Update score and HP trackers
                scoreTrackerText.text = score.ToString();
                UpdateHealth();
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

    public void UpdateHealthWithDamage()
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

    public void UpdateHealth()
    {
        switch (currentHP)
        {
            case 3:
                livesAnimator.Play("3");
                break;
            case 2:
                livesAnimator.Play("2");
                break;
            case 1:
                livesAnimator.Play("1");
                break;
            case 0:
                livesAnimator.Play("0");
                break;
            default:
                Debug.Log("Lives Error");
                break;
        }
    }
}
