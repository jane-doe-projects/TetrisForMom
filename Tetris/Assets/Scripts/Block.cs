using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Vector3 rotationPoint;

    private float previousStrafeTime;
    private float previousTime;
    public float fallTime = 0.8f;
    private float strafeTime = 0.1f;

    public static int gridH = 20;
    public static int gridW = 10;

    public static Transform[,] grid = new Transform[gridW, gridH];

    // Start is called before the first frame update
    void Start()
    {
        fallTime = GameManager.Instance.currentFallTime;
        if (!ValidPosition())
        {
            GameManager.Instance.GameOver();
            Debug.Log("Game Over");
            // TODO do other stuff
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    bool ValidPosition()
    {
        foreach(Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if(roundedX < 0 || roundedX >= gridW || roundedY < 0 || roundedY >= gridH)
            {
                return false;
            }

            if (grid[roundedX, roundedY] != null)
            {
                return false;
            }
        }

        return true;
    }

    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX, roundedY] = children;
        }
    }

    void HandleInput()
    {
        if (Input.GetKey(KeyCode.A) && Time.time - previousStrafeTime > strafeTime || Input.GetKey(KeyCode.LeftArrow) && Time.time - previousStrafeTime > strafeTime)
        {
            transform.position += new Vector3(-1, 0, 0);

            if (!ValidPosition())
                transform.position += new Vector3(1, 0, 0);
            previousStrafeTime = Time.time;
        }

        else if ((Input.GetKey(KeyCode.D) && Time.time - previousStrafeTime > strafeTime) || (Input.GetKey(KeyCode.RightArrow) && Time.time - previousStrafeTime > strafeTime))
        {
            transform.position += new Vector3(1, 0, 0);

            if (!ValidPosition())
                transform.position += new Vector3(-1, 0, 0);
            previousStrafeTime = Time.time;
        }

        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            if (!ValidPosition())
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);

        }


        if ((Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime)) || (Time.time - previousTime > (Input.GetKey(KeyCode.S) ? fallTime / 10 : fallTime)))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidPosition())
            {
                transform.position += new Vector3(0, 1, 0);
                AddToGrid();
                CheckForLines();
                this.enabled = false;
                FindObjectOfType<SpawnControl>().SpawnRandomBlock();
                GameManager.Instance.playerScore.IncreaseScoreCount();
            }
                
            previousTime = Time.time;
        }
    }

    void CheckForLines()
    {
        for (int i = gridH-1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }

    bool HasLine(int i)
    {
        for(int j = 0; j < gridW; j++)
        {
            if (grid[j, i] == null)
                return false;
        }
        return true;
    }

    void DeleteLine(int i)
    {
        for (int j = 0; j < gridW; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
        GameManager.Instance.playerScore.IncreaseLineCount();
    }

    void RowDown(int i)
    {
        for(int y = i; y < gridH; y++)
        {
            for(int j = 0; j < gridW; j++)
            {
                if (grid[j,y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0,1,0);
                }
            }
        }
    }
}
