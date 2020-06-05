using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    #region Singleton
    public static Spawner instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public GameObject[] pieces;

    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }

    public void Generate()
    {
        int rand = Random.Range(0, pieces.Length);
        Instantiate(pieces[rand], transform.position, Quaternion.identity);
    }
}
