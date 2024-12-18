﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public static string FinalScoreText; 

    public static Transform[,] gameBoard = new Transform[10, 20];
    private static int numRowsDel = 0;
    private static int _score = 0;

    static int level = 1;
    static int lineClearsNeeded = 5;
    static int levelLineClearsNeeded = 5;

    public static int GetRowsDel()
    {
        return numRowsDel;
    }

    public static void SetToZero()
    {
        numRowsDel = 0;
    }

    public static void NextLevelCheck()
    {
        if (lineClearsNeeded == 0)
        {
            level++;
            levelLineClearsNeeded += 5;

            Shape.IncreaseSpeed(level);

            var textL = GameObject.Find("Level").GetComponent<Text>();
            textL.text = level.ToString();

            var textC = GameObject.Find("LinesToClear").GetComponent<Text>();
            textC.text = levelLineClearsNeeded.ToString();
            lineClearsNeeded = levelLineClearsNeeded;
        }
    }

    public static void ClearLine()
    {
        lineClearsNeeded--;
        var textC = GameObject.Find("LinesToClear").GetComponent<Text>();
        textC.text = lineClearsNeeded.ToString();
    }

    public static bool DeleteFullRows()
    {
        for (int r = 0; r < 20; r++)
        {
            if (IsFullRow(r))
            {
                DeleteRow(r);
                numRowsDel++;

                AudioManager.Instance.PlayOneShot(AudioManager.Instance.rowDelete);
                Game.ClearLine();
                Game.NextLevelCheck();

                Debug.Log(numRowsDel);
                return true;
            }
        }
        return false;
    }

    public static bool IsFullRow(int row)
    {
        for (int c = 0; c < 10; ++c)
        {
            try
            {
                if (gameBoard[c, row] == null)
                {
                    return false;
                }
            }
            catch (System.IndexOutOfRangeException)
            {
                Debug.Log("rows: " + row + "Columns" + c);
            }
        }
        return true;
    }

    public static void DeleteRow(int row)
    {
        for (int c = 0; c < 10; ++c)
        {
            try
            {
                Destroy(gameBoard[c, row].gameObject);
                gameBoard[c, row] = null;
            }
            catch (System.IndexOutOfRangeException)
            {
                Debug.Log("rows: " + row + "Columns" + c);
            }
        }
        row++;

        Debug.Log("Deleted row" + row);

        for (int i = row; i < 20; ++i)
        {
            for (int c = 0; c < 10; ++c)
            {
                try
                {
                    if (gameBoard[c, i] != null)
                    {
                        gameBoard[c, i - 1] = gameBoard[c, i];
                        gameBoard[c, i] = null;
                        gameBoard[c, i - 1].position += new Vector3(0, -1, 0);
                    }
                }
                catch (System.IndexOutOfRangeException)
                {
                    Debug.Log("rows: " + row + "Columns" + c);
                }
            }
        }
        Debug.Log("Shifted rows down");
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");

        // Set FinalScoreText to the current score display
        FinalScoreText = GameObject.Find("Score").GetComponent<Text>().text;

        // Load the game over scene
        SceneManager.LoadScene("GameOver");
    }
}
