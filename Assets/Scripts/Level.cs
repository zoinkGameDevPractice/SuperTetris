using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class Level : ScriptableObject
{
    public int linesToClear;
    public float speed;
    public bool canBeBoosted;
}
