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

    GameObject p;
    GameObject heldObject;
    GameObject objToHold;

    [HideInInspector] public bool canHold = false;

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
            if (heldObject && canHold)
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
        heldObject = MatchPiece(p);
        HoldImage.instance.SetSprite(heldObject.GetComponent<Piece>().sprite);
        Destroy(p);
        Generate();
    }

    void Swap()
    {
        objToHold = MatchPiece(p);
        Destroy(p);
        p = Instantiate(heldObject, transform.position, Quaternion.identity);
        heldObject = objToHold;
        HoldImage.instance.SetSprite(heldObject.GetComponent<Piece>().sprite);
        canHold = false;
    }

    GameObject MatchPiece(GameObject p)
    {
        foreach(GameObject obj in pieces)
        {
            if(obj.tag == p.tag)
            {
                return obj;
            }
        }
        return null;
    }
}
