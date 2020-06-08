using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    #region Singleton
    public static LevelManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField] private List<Level> levels = new List<Level>();

    private Level currentLevel;
    private int currentLevelIndex;

    public TextMeshProUGUI levelText;

    public delegate void OnLevelChanged();
    public OnLevelChanged onLevelChanged;

    private int clearedLines;

    // Start is called before the first frame update
    void Start()
    {
        currentLevel = levels[0];
        currentLevelIndex = 0;
    }

    void SetNextLevel()
    {
        if (currentLevelIndex + 1 > levels.Count)
            Win();
        int newIndex = currentLevelIndex + 1;
        currentLevel = levels[newIndex];
        currentLevelIndex = newIndex;
        levelText.text = (currentLevelIndex + 1).ToString();
        if (onLevelChanged != null)
            onLevelChanged.Invoke();
    }

    public void IncrementLines()
    {
        clearedLines++;
        if (clearedLines >= currentLevel.linesToClear)
        {
            SetNextLevel();
        }
    }

    void Win()
    {
        Time.timeScale = 0;
        print("You win");
    }

    #region Getters
    public Level GetCurrentLevel()
    {
        return currentLevel;
    }

    public int GetCurrentLevelIndex()
    {
        return currentLevelIndex;
    }
    #endregion
}
