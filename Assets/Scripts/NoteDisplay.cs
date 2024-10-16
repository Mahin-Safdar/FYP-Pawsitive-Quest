using UnityEngine;
using UnityEngine.UI;

public class NoteDisplay : MonoBehaviour
{
    public GameObject noteCanvas;   // The canvas that will show the note
    public Image noteImageField;    // The image field to display note background
    public Sprite noteImage;        // The unique image for this note
    public Button closeButton;      // The button to close the note

    void Start()
    {
        // Make sure the canvas is hidden at the start of the game
        noteCanvas.SetActive(false);

        // Ensure the close button works by assigning the HideNote function to it
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(HideNote);
        }
        else
        {
            Debug.LogError("Close button is not assigned in the Inspector.");
        }
    }

    // Method to display the note
    public void ShowNote()
    {
        noteImageField.sprite = noteImage;  // Set the note's image from the stored data
        noteCanvas.SetActive(true);         // Activate the canvas to display the note
    }

    // Method to hide the note
    public void HideNote()
    {
        noteCanvas.SetActive(false);        // Deactivate the canvas to hide the note
    }
}
