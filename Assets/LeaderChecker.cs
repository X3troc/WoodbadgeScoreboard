using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class LeaderChecker : MonoBehaviour
{
    public GameObject[] playerGameObjects; // Array to track player game objects
    private Dictionary<GameObject, float> playerScores = new Dictionary<GameObject, float>(); // Dictionary to track scores

    void Start()
    {
        StartCoroutine(LogScoresEveryMinute()); // Start the coroutine to log scores
    }

    // Update is called once per frame
    void Update()
    {
        GameObject leader = null;
        float highestY = float.MinValue;
        bool isTie = false;

        foreach (GameObject player in playerGameObjects)
        {
            float playerY = player.transform.position.y;
            playerScores[player] = playerY; // Update the score based on Y position

            if (playerY > highestY)
            {
                highestY = playerY;
                leader = player;
                isTie = false;
            }
            else if (playerY == highestY)
            {
                isTie = true;
            }
        }

        if (isTie)
        {
            leader = null; // No leader if there's a tie
        }

        // Handle the leader (or lack thereof) as needed
        foreach (GameObject player in playerGameObjects)
        {
            PlayerMarker playerMarker = player.GetComponent<PlayerMarker>();
            if (playerMarker != null)
            {
                playerMarker.SetIsWinning(player == leader);
            }
        }
    }

    private IEnumerator LogScoresEveryMinute()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f); // Wait for 60 seconds
            LogScores();
        }
    }

    private void LogScores()
    {
        string logFilePath = Path.Combine(Application.persistentDataPath, "scores.log");
        using (StreamWriter writer = new StreamWriter(logFilePath, true))
        {
            writer.WriteLine("--------------------------------------------");
            writer.WriteLine($"Time: {System.DateTime.Now}");
            foreach (var playerScore in playerScores)
            {
                writer.WriteLine($"Player: {playerScore.Key.name}, Score: {playerScore.Value}");
            }
            writer.WriteLine();
        }
        Debug.Log($"Logged scores to {logFilePath}");
    }
}