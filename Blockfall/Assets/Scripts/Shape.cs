using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using InputMapperPlugin; // Import the InputMapper namespace

public class Shape : MonoBehaviour
{
    public static float speed = 1.0f;
    private float lastMovedDown = 0;
    private int spaceFromBottom = 0;
    private float keyDelay = 2f;
    private float timePassed = 0f;

    Vector2 previousPosition = Vector2.zero;
    Vector2 direction = Vector2.zero;

    bool moved = false;

    void Start()
    {
        if (!IsInGrid())
        {
            AudioManager.Instance.PlayOneShot(AudioManager.Instance.gameOver);
            Invoke("OpenGameOverScene", 0.5f);
        }
    }

    void OpenGameOverScene()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("GameOver");
    }

    public static void IncreaseSpeed(int level)
    {
        Shape.speed -= 0.005f * level;
    }

    void Update()
    {
        CheckUserInput();
    }

    void CheckUserInput()
    {
        // Get movement input from InputMapper
        Vector2 movementInput = InputMapper.GetMovementInput();

        if (movementInput.x < 0)
        {
            MoveLeft();
        }
        else if (movementInput.x > 0)
        {
            MoveRight();
        }

        // Check for drop input
        if (InputMapper.IsDropPressed())
        {
            DropDown();
        }

        // Check for rotate input
        if (InputMapper.IsRotatePressed())
        {
            Rotate();
        }

        timePassed += Time.deltaTime;

        // Move down based on speed or user input
        if (movementInput.y < 0 || Time.time - lastMovedDown >= Shape.speed)
        {
            MoveDown();
        }

        // Check for save shape input
        if (Input.GetKeyDown(KeyCode.LeftShift)) // Optional: Keep specific keyboard keys for non-standard actions
        {
            SaveShape();
        }
    }

    void MoveLeft()
    {
        transform.position += new Vector3(-1, 0, 0);

        if (!IsInGrid())
        {
            transform.position += new Vector3(1, 0, 0);
        }
        else
        {
            UpdateGame();
            AudioManager.Instance.PlayOneShot(AudioManager.Instance.shapeMove);
        }
    }

    void MoveRight()
    {
        transform.position += new Vector3(1, 0, 0);

        if (!IsInGrid())
        {
            transform.position += new Vector3(-1, 0, 0);
        }
        else
        {
            UpdateGame();
            AudioManager.Instance.PlayOneShot(AudioManager.Instance.shapeMove);
        }
    }

    void MoveDown()
    {
        transform.position += new Vector3(0, -1, 0);
        if (!IsInGrid())
        {
            transform.position += new Vector3(0, 1, 0);

            bool rowDeleted = Game.DeleteFullRows();

            if (rowDeleted)
            {
                Game.DeleteFullRows();
                IncreaseScore();
            }

            enabled = false;
            tag = "Untagged";

            FindObjectOfType<ShapeSpawner>().SpawnShape();
        }
        else
        {
            UpdateGame();
            AudioManager.Instance.PlayOneShot(AudioManager.Instance.shapeMove);
        }

        spaceFromBottom++;
        lastMovedDown = Time.time;
    }

    void Rotate()
    {
        transform.Rotate(0, 0, 90);

        if (!IsInGrid())
        {
            transform.Rotate(0, 0, -90);
        }
        else
        {
            UpdateGame();
            AudioManager.Instance.PlayOneShot(AudioManager.Instance.rotateSound);
        }
    }

    void DropDown()
    {
        transform.position = FindObjectOfType<GhostShape>().GetPosition();
        if (!IsInGrid())
        {
            bool rowDeleted = Game.DeleteFullRows();

            if (rowDeleted)
            {
                Game.DeleteFullRows();
                IncreaseScore();
            }

            enabled = false;
            tag = "Untagged";

            FindObjectOfType<ShapeSpawner>().SpawnShape();
        }
        else
        {
            UpdateGame();
            AudioManager.Instance.PlayOneShot(AudioManager.Instance.shapeMove);
        }

        lastMovedDown = 0;
    }

    public void SaveShape()
    {
        GameObject save = GameObject.FindGameObjectWithTag("CurrentActiveShape");
        FindObjectOfType<ShapeSpawner>().SaveShape(save.transform);
    }

    public bool IsInGrid()
    {
        foreach (Transform childBlock in transform)
        {
            Vector2 vect = RoundVector(childBlock.position);

            if (!IsInBorder(vect))
            {
                return false;
            }

            if (Game.gameBoard[(int)vect.x - 1, (int)vect.y - 1] != null &&
                Game.gameBoard[(int)vect.x - 1, (int)vect.y - 1].parent != transform)
            {
                return false;
            }
        }
        return true;
    }

    public static bool IsInBorder(Vector2 pos)
    {
        return ((int)pos.x >= 1 &&
            (int)pos.x <= 10 &&
            (int)pos.y >= 1);
    }

    public void UpdateGame()
    {
        for (int y = 0; y < 20; ++y)
        {
            for (int x = 0; x < 10; ++x)
            {
                if (Game.gameBoard[x, y] != null &&
                    Game.gameBoard[x, y].parent == transform)
                {
                    Game.gameBoard[x, y] = null;
                }
            }
        }

        foreach (Transform childBlock in transform)
        {
            Vector2 vect = RoundVector(childBlock.position);

            Game.gameBoard[(int)vect.x - 1, (int)vect.y - 1] = childBlock;
        }
    }

    public Vector2 RoundVector(Vector2 position)
    {
        return new Vector2(Mathf.Round(position.x), Mathf.Round(position.y));
    }

    public void IncreaseScore()
    {
        var textComp = GameObject.Find("Score").GetComponent<Text>();
        int score = int.Parse(textComp.text);

        score += 100;

        textComp.text = score.ToString();
    }
}
