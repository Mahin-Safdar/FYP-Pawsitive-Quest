using UnityEngine;
using TMPro;

public class UI_Chest : MonoBehaviour
{
    public GameObject uiPanel;           // Reference to the UI Panel containing the input field and button
    public TMP_InputField codeInputField; // Reference to the input field for code entry
    public Chest chest;                  // Reference to the Chest script

    private void Start()
    {
        uiPanel.SetActive(false); // Hide the UI panel at the start
    }

    // Call this method to show the UI
    public void ShowChestUI()
    {
        codeInputField.text = ""; // Clear any previous input

        // Determine the type of chest to display appropriate instructions
        if (chest != null)
        {
            if (chest.chestType == Chest.ChestType.Code)
            {
                // Show the UI for code input
                uiPanel.SetActive(true);
            }
            else if (chest.chestType == Chest.ChestType.Key)
            {
                // Handle key unlocking logic if needed
                Debug.Log("You need to unlock this chest with a key.");
                // Optionally, show a different panel or message
            }
        }
    }

    // Call this method when the player submits their code
    public void OnSubmitCode()
    {
        if (chest != null) // Ensure the chest reference is valid
        {
            chest.TryUnlockChest(codeInputField.text); // Pass the entered code to the chest
        }
        else
        {
            Debug.LogError("Chest reference is null in UI_Chest.");
        }
        uiPanel.SetActive(false); // Hide the UI after submission
    }
}
