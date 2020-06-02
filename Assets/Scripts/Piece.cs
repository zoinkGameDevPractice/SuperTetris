using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    float fallSpeed = 0.8f;
    float fallTimer;

    float move;
    public float moveDelay = 0.4f;
    float moveTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxisRaw("Horizontal");
        moveTimer += Time.deltaTime;
        fallTimer += Time.deltaTime;
        if(move != 0 && moveTimer > moveDelay)
        {
            transform.position += new Vector3(move, 0);
            moveTimer = 0;
        }
        if(fallTimer > fallSpeed)
        {
            Fall();
            fallTimer = 0;
        }
    }

    void Fall()
    {
        transform.position += new Vector3(0, -1f);
    }
}
