using System.Collections;
using UnityEngine;
using TMPro;  // If you're using TextMeshPro for displaying the timer
using UnityEngine.SceneManagement; // For loading next scene or ending the game

public class Timer : MonoBehaviour
{
    public float timeLimit = 25 * 60f;  // 25 minutes in seconds
    private float currentTime;

    public TextMeshProUGUI timerText;  // Optional: Drag your TextMeshPro object here in the Inspector

    void Start()
    {
        currentTime = timeLimit;
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;  // Decrease the timer each frame
            UpdateTimerDisplay();  // Optional: Update the displayed timer
        }
        else
        {
            EndLevel();
        }
    }

    void UpdateTimerDisplay()
    {
        // Optional: Display the remaining time in minutes and seconds
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);  // Example: 25:00
    }

    void EndLevel()
    {
        
        Debug.Log("Time's up! Level ended.");
        SceneManager.LoadScene("Game_Over");  
    }
}
