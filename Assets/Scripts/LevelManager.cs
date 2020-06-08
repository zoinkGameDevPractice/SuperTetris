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

    // Start is called before the first frame update
    void Start()
    {
        currentLevel = levels[0];
        currentLevelIndex = 0;
        foreach (Level level in levels)
            level.onLineCleared += SetNextLevel;
    }

    void SetNextLevel()
    {
        int newIndex = currentLevelIndex + 1;
        currentLevel = levels[newIndex];
        currentLevelIndex = newIndex;
        levelText.text = (currentLevelIndex + 1).ToString();
        if (onLevelChanged != null)
            onLevelChanged.Invoke();
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
