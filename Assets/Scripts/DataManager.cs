using UnityEngine;

public class DataManager : MonoBehaviour
{
    // Singleton
    public static DataManager instance;

    [Header("Stored Data:")]
    public int savedScore;
    public int savedHP;
    public int savedCharacter;

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
    }

    public void StoreSavedScore(int currentScore)
    {
        savedScore = currentScore;
    }

    public void StoreSavedHP(int currentHP)
    {
        savedHP = currentHP;
    }

    public void StoreSavedCharacter(int currentCharacter)
    {
        savedCharacter = currentCharacter;
    }
}
