using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class InteractiveChoices : MonoBehaviour
{
    public GameObject dialogueCanvas;  // Canvas for dialogue
    public TMP_Text dialogueText;      // Dialogue text display
    public Button option1Button;       // Button for option 1
    public Button option2Button;       // Button for option 2

    public InventoryManager inventoryManager;  // Reference to the player's inventory

    [System.Serializable]
    public struct DialogueOption
    {
        public string initialDialogue;  // NPC's initial dialogue
        public string option1Text;      // Option 1 text
        public string option2Text;      // Option 2 text
        public string response1;        // Response for option 1
        public string response2;        // Response for option 2
    }

    public DialogueOption npcDialogue;  // Dialogue data

    // Required items for this NPC and the reward
    public List<string> requiredItems;  // List of item names this NPC requires
    public string reward;  // NPC reward after providing items (e.g., a code)

    void Start()
    {
        dialogueCanvas.SetActive(false); // Hide dialogue canvas initially
        option1Button.onClick.AddListener(HandleOption1);
        option2Button.onClick.AddListener(HandleOption2);
    }

    // Method to handle starting the dialogue
    public void StartDialogue()
    {
        if (dialogueCanvas == null || dialogueText == null || option1Button == null || option2Button == null)
        {
            Debug.LogError("Some UI elements are not assigned.");
            return;
        }

        dialogueText.text = npcDialogue.initialDialogue;  // Set the initial dialogue
        option1Button.GetComponentInChildren<TextMeshProUGUI>().text = npcDialogue.option1Text;
        option2Button.GetComponentInChildren<TextMeshProUGUI>().text = npcDialogue.option2Text;

        dialogueCanvas.SetActive(true);  // Show the dialogue canvas
    }

    // Option 1 button logic (item check and reward)
    private void HandleOption1()
    {
        if (HasRequiredItems())
        {
            Debug.Log("All required items found!");

            // Use each required item individually
            foreach (string item in requiredItems)
            {
                Debug.Log("Using item: " + item);
                inventoryManager.UseItem(item);  // Use each required item
            }

            // Add the reward to the player's inventory
            inventoryManager.AddItem(new GameObject(reward)); // Create a new GameObject with the reward name
            dialogueText.text = "You have everything I need! Here is your reward: " + reward;

            Debug.Log("Reward given: " + reward);
            Invoke("HideDialogue", 2f);  // Hide dialogue after 2 seconds
        }
        else
        {
            Debug.Log("Missing items, cannot give reward.");
            dialogueText.text = npcDialogue.response1;  // Respond if player lacks items
            Invoke("HideDialogue", 2f);
        }
    }

    // Option 2 button logic (regular response)
    private void HandleOption2()
    {
        dialogueText.text = npcDialogue.response2;  // Regular response for option 2
        Debug.Log("Option 2 selected: " + npcDialogue.response2);
        Invoke("HideDialogue", 2f);
    }

    // Check if the player has all required items and the correct quantity
    private bool HasRequiredItems()
    {
        foreach (var item in requiredItems)
        {
            if (!inventoryManager.HasItem(item, 1))  // Assuming quantity 1 is required for each item
            {
                Debug.Log("Missing item: " + item);
                return false;  // Player is missing an item
            }
        }
        return true;
    }

    private void HideDialogue()
    {
        dialogueCanvas.SetActive(false);  // Hide the canvas
    }

    void Update()
    {
        // Detect interaction with the NPC (raycast or collider-based)
        if (Input.GetMouseButtonDown(0))  // Detect left mouse button click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("NPC"))  // Ensure NPC has the correct tag
                {
                    StartDialogue();  // Start NPC dialogue
                }
            }
        }
    }
}
