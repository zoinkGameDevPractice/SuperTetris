using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public float fallSpeed = 0.7f;
    public float fallBoost = 0.6f;
    float fallTimer;
    float newFallSpeed;
    bool accelerate = false;

    public Vector3 rotatePoint;

    public float moveDelay = 0.1f;
    float move;
    float moveTimer;

    static int height = 20;
    static int width = 10;

    // Update is called once per frame
    void Update()
    {
        Timers();
        InputCalculation();
        MoveCalculation();
        FallCalculation();
    }

    void Fall()
    {
        transform.position += new Vector3(0, -1f);
        if(!ValidMove())
        {
            transform.position -= new Vector3(0, -1f);
        }
    }

    bool ValidMove()
    {
        foreach(Transform child in transform)
        {
            if(Mathf.RoundToInt(child.position.x) >= width || Mathf.RoundToInt(child.position.x) < 0)
            {
                return false;
            } else if(Mathf.RoundToInt(child.position.y) < 0 || Mathf.RoundToInt(child.position.y) >= height)
            {
                return false;
            }
        }

        return true;
    }

    void InputCalculation()
    {
        move = Input.GetAxisRaw("Horizontal");

        if (Input.GetAxisRaw("Vertical") == -1)
            accelerate = true;

        else
            accelerate = false;

        if (Input.GetKeyDown(KeyCode.K))
            Rotate(0);
        if (Input.GetKeyDown(KeyCode.L))
            Rotate(1);
    }

    void MoveCalculation()
    {
        if (move != 0 && moveTimer > moveDelay)
        {
            transform.position += new Vector3(move, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(move, 0);
            }
            moveTimer = 0;
        }
    }

    void FallCalculation()
    {
        if (accelerate)
        {
            newFallSpeed = fallSpeed - fallBoost;
        }
        else
            newFallSpeed = fallSpeed;
        if (fallTimer > newFallSpeed)
        {
            Fall();
            fallTimer = 0;
        }
    }

    void Rotate(int dir)
    {
        if(dir == 0)
        {
            transform.RotateAround(transform.TransformPoint(rotatePoint), new Vector3(0, 0, 1), -90f);
            if(!ValidMove())
            {
                transform.RotateAround(transform.TransformPoint(rotatePoint), new Vector3(0, 0, 1), 90f);
            }
        }

        if(dir == 1)
        {
            transform.RotateAround(transform.TransformPoint(rotatePoint), new Vector3(0, 0, 1), 90f);
            if(!ValidMove())
            {
                transform.RotateAround(transform.TransformPoint(rotatePoint), new Vector3(0, 0, 1), -90f);
            }
        }
    }

    void Timers()
    {
        moveTimer += Time.deltaTime;
        fallTimer += Time.deltaTime;
    }
}
