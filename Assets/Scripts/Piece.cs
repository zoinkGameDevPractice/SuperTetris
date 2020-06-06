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

    public float gracePeriod = 0.1f;
    float graceTimer;

    static int width = 10;
    static int height = 20;
    static Transform[,] grid = new Transform[width, height];

    #region Unity Functions
    private void Start()
    {
        if(!ValidMove())
        {
            transform.position -= new Vector3(1, 0);
            if(!ValidMove())
            {
                transform.position += new Vector3(2, 0);
                if(!ValidMove())
                {
                    enabled = false;
                    Lose();
                }
            }
        }
    }

    void Update()
    {
        Timers();
        InputCalculation();
        MoveCalculation();
        FallCalculation();
    }
    #endregion

    #region Input
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
    #endregion

    #region Movement
    bool ValidMove()
    {
        foreach(Transform child in transform)
        {
            int x = Mathf.RoundToInt(child.position.x);
            int y = Mathf.RoundToInt(child.position.y);

            if (x >= width || x < 0)
            {
                return false;
            } else if(y < 0 || y >= height)
            {
                return false;
            }
       
            if (grid[x, y] != null)
                return false;
        }

        return true;
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
            graceTimer = 0;
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

    void Fall()
    {
        transform.position += new Vector3(0, -1f);
        if(!ValidMove())
        {
            transform.position -= new Vector3(0, -1f);
            if(graceTimer > gracePeriod)
            {
                AddToGrid();
                CheckForLines();
                Spawner.instance.Generate();
                enabled = false;
            }
        }
    }

    void Rotate(int dir)
    {
        float angle = 0f;
        if (dir == 0)
            angle = -90f;
        if (dir == 1)
            angle = 90f;
        transform.RotateAround(transform.TransformPoint(rotatePoint), new Vector3(0, 0, 1), angle);
        if(!ValidMove())
        {
            transform.RotateAround(transform.TransformPoint(rotatePoint), new Vector3(0, 0, 1), -angle);
        }
        graceTimer = 0;
    }
    #endregion

    #region Grid & Lines
    void AddToGrid()
    {
        foreach(Transform child in transform)
        {
            grid[Mathf.RoundToInt(child.transform.position.x), Mathf.RoundToInt(child.transform.position.y)] = child;
        }
    }

    void CheckForLines()
    {
        for(int i = height -1; i >= 0; i--)
        {
            if(HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }

    bool HasLine(int i)
    {
        for(int j = 0; j < width; j++)
        {
            if(grid[j, i] == null)
            {
                return false;
            }
        }

        return true;
    }

    void DeleteLine(int i)
    {
        for(int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }

    void RowDown(int i)
    {
        for(int y = i; y < height; y++)
        {
            for(int j = 0; j < width; j++)
            {
                if(grid[j, y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1);
                }
            }
        }
    }
    #endregion

    void Timers()
    {
        moveTimer += Time.deltaTime;
        fallTimer += Time.deltaTime;
        graceTimer += Time.deltaTime;
    }

    void Lose()
    {
        Spawner.instance.Lose();
    }
}