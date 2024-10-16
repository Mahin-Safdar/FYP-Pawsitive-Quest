using UnityEngine;
using UnityEngine.SceneManagement; // Include SceneManagement for level loading

public class Chest : MonoBehaviour
{
    public enum ChestType { Key, Code } // Define types of chests
    public ChestType chestType; // The type of chest
    public string codeToUnlock; // The code required to unlock the chest
    public bool isLocked = true; // Initially locked state
    public GameObject contents; // The contents of the chest

    // References for animations and audio
    private Animator animator; // Reference to the Animator component
    private AudioSource audioSource; // Reference to the AudioSource component

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Method to unlock the chest with a key
    public void UnlockWithKey()
    {
        if (isLocked && chestType == ChestType.Key)
        {
            isLocked = false;
            PlayOpenAnimation(); // Use the open trigger
            PlayUnlockSound();
            Debug.Log("Chest unlocked with a key!");
            // Logic for opening the chest, e.g., show contents
            if (contents != null) contents.SetActive(true);
        }
    }

    // Method to unlock the chest with a code
    public void TryUnlockChest(string playerInputCode)
    {
        if (isLocked && chestType == ChestType.Code)
        {
            if (playerInputCode == codeToUnlock)
            {
                isLocked = false;
                PlayOpenAnimation(); // Use the open trigger
                PlayUnlockSound();
                Debug.Log("Chest unlocked with code!");
                // Logic for opening the chest
                if (contents != null) contents.SetActive(true);

                // Trigger level transition only if this is the chest unlocked by code
                LoadNextLevel(); // Call the transition here only for code chests
            }
            else
            {
                Debug.Log("Incorrect code.");
            }
        }
        else if (isLocked && chestType == ChestType.Key)
        {
            Debug.Log("This chest requires a key.");
        }
    }

    private void PlayOpenAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Open"); // Use your "Open" trigger
        }
    }

    private void PlayUnlockSound()
    {
        if (audioSource != null)
        {
            audioSource.Play(); // Play the unlock sound
        }
    }

    private void LoadNextLevel()
    {
        // Only transition if the chest type is Code
        if (chestType == ChestType.Code)
        {
            Debug.Log("Loading EasyLevel_2...");
            SceneManager.LoadScene("EasyLevel_2");  // Directly load the next level
        }
    }
}
