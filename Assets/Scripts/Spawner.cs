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

    public List<GameObject> pieces = new List<GameObject>();
    List<GameObject> possibilities = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        possibilities.AddRange(pieces);
        Generate();
    }

    public void Generate()
    {
        int rand = Random.Range(0, possibilities.Count);
        GameObject p = Instantiate(possibilities[rand], transform.position, Quaternion.identity);
        foreach(GameObject obj in possibilities)
        {
            if(obj.tag == p.tag)
            {
                possibilities.Remove(obj);
                if (possibilities.Count == 0)
                {
                    possibilities = new List<GameObject>();
                    possibilities.AddRange(pieces);
                }
                return;
            }
        }
    }
}
