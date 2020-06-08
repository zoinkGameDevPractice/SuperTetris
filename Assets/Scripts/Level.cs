using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class Level : ScriptableObject
{
    public int linesToClear;
    public float speed;
    public bool canBeBoosted;

    private int clearedLines;

    public delegate void OnLineCleared();
    public OnLineCleared onLineCleared;

    public void IncrementLines()
    {
        clearedLines++;
        if(clearedLines >= linesToClear)
        {
            if (onLineCleared != null)
                onLineCleared.Invoke();
        }
    }
}
