using UnityEngine;
using TMPro;

public class PlayerUIManager : MonoBehaviour
{
    public TMP_Text scoreTrackerText;
    public int score;

    private void Start()
    {
        score = 0;
        scoreTrackerText.text = score.ToString();
    }

    public void IncreaseScore()
    {
        score++;
        scoreTrackerText.text = score.ToString();
    }
}
