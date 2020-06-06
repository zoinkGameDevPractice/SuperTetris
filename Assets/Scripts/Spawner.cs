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

    public GameObject gameOver;

    public GameObject p;
    GameObject heldObject;

    // Start is called before the first frame update
    void Start()
    {
        possibilities.AddRange(pieces);
        Generate();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (!heldObject)
            {
                Hold();
                return;
            }
            if (heldObject)
            {
                Swap();
                return;
            }
        }
    }

    public void Generate()
    {
        int rand = Random.Range(0, possibilities.Count);
        p = Instantiate(possibilities[rand], transform.position, Quaternion.identity);
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

    public void Lose()
    {
        gameOver.transform.Find("Canvas").gameObject.SetActive(true);
    }

    void Hold()
    {
        heldObject = p;
        Destroy(p);
        Generate();
    }

    void Swap()
    {
        GameObject temp = p;
        Destroy(p);
        Instantiate(heldObject, transform.position, Quaternion.identity);
        heldObject = temp;
    }
}
