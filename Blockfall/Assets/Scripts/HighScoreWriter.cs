using System.IO;
using UnityEngine;

public class HighScoreWriter : MonoBehaviour
{
    private const string FilePath = "highscore.txt";

    public void WriteHighScore(int highScore)
    {
        if (!File.Exists(FilePath))
        {
            File.Create(FilePath).Dispose();
        }

        File.WriteAllText(FilePath, $"High Score: {highScore}");
        Debug.Log($"High Score saved to {FilePath}: {highScore}");
    }
}
