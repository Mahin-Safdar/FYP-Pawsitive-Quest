using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    public int startingLives = 5;
    public Transform player;
    public TMP_Text livesText; 
    private Vector3 respawnPosition; 
    private int currentLives;
    private bool isRespawning = false; // a flag to prevent losing another life on respawn

    void Start()
    {
        currentLives = startingLives;
        respawnPosition = player.position; // Initialize with player's starting position
        UpdateLivesUI(); // Update UI at the start
    }

    // Call this method when the player hits a checkpoint
    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        respawnPosition = checkpointPosition; // Set the respawn position to the last checkpoint
        Debug.Log("Checkpoint updated to: " + checkpointPosition); 
    }

    //  respawn the player
    public void RespawnPlayer()
    {
        if (!isRespawning && currentLives > 0) // Ensure respawning only once
        {
            isRespawning = true; // Set flag to prevent multiple respawns
            // Move the player to the last checkpoint
            player.position = respawnPosition;
            Debug.Log("Player respawned at: " + respawnPosition); // Debug log to confirm respawn location

            StartCoroutine(RespawnCooldown()); // Allow a short delay before another life is lost
        }
    }

    // This method decreases the player's lives
    public void LoseLife()
    {
        if (currentLives > 0)
        {
            currentLives--; // Decrease lives
            UpdateLivesUI(); // Update the UI to reflect the current lives

            if (currentLives <= 0)
            {
                GameOver(); // Trigger game over if lives reach zero
            }
            else
            {
                RespawnPlayer(); // Respawn the player if they still have lives
            }
        }
    }

    private void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = currentLives.ToString(); // Update the lives text on the UI
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over! Restart from the beginning.");
        SceneManager.LoadScene("Game_Over");
    }

    // Cooldown to prevent losing another life instantly
    private IEnumerator RespawnCooldown()
    {
        yield return new WaitForSeconds(1.0f); // Short delay to avoid instant life loss
        isRespawning = false; // Reset flag after the delay
    }
}
