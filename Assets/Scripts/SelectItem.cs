using UnityEngine;
using System.Collections.Generic;

public class SelectItem : MonoBehaviour
{
    public float interactionRange = 3f;  // Range within which the player can interact with items
    public LayerMask interactableLayer;  // Layer of interactable objects
    public InventoryManager inventoryManager;  // Reference to the InventoryManager
    public UI_Chest uiChest; // Reference to the UI_Chest script to manage the chest UI

    private void Start()
    {
        if (inventoryManager == null)
        {
            Debug.LogError("InventoryManager is not assigned at start!");
        }
        if (uiChest == null)
        {
            Debug.LogError("UI_Chest is not assigned at start!");
        }
    }

    void Update()
    {
        // Perform a raycast from the camera's position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, interactionRange, interactableLayer);

        if (hits.Length > 0)
        {
            foreach (RaycastHit hit in hits)
            {
                if (Input.GetMouseButtonDown(0)) // Left mouse button
                {
                    // Handle interaction with the hit object
                    if (hit.collider != null)
                    {
                        Debug.Log("Hit object: " + hit.collider.gameObject.name); // Log the hit object
                        GameObject hitObject = hit.collider.gameObject;

                        ItemSelect(hitObject); // Handle item selection
                        HandleChestInteraction(hitObject); // Handle chest unlocking if applicable
                        DisplayNoteIfApplicable(hitObject); // Display note if applicable
                        TriggerDialogueOfNPC(hitObject); // Trigger NPC dialogue if applicable
                    }
                }
            }
        }
    }

    // Unlock the chest by invoking the Chest component's method

    public void HandleChestInteraction(GameObject item)
    {
        // Check if the hit object has a Chest component
        Chest chest = item.GetComponent<Chest>();
        if (chest != null)
        {
            // Check if the chest requires a key
            if (chest.chestType == Chest.ChestType.Key)
            {
                // Attempt to unlock with key
                if (inventoryManager.HasAnyKey()) // Assuming you have a method to check for a key
                {
                    chest.UnlockWithKey();
                }
                else
                {
                    Debug.Log("You need a key to unlock this chest.");
                }
            }
            else if (chest.chestType == Chest.ChestType.Code)
            {
                // Show UI for code input
                FindObjectOfType<UI_Chest>().ShowChestUI(); // Call the UI method to show the chest UI
            }
        }
    }

    // Handle item selection for collectible items
    void ItemSelect(GameObject item)
    {
        if (item.CompareTag("Collectible"))
        {
            // Add the item to the inventory
            if (inventoryManager != null)
            {
                inventoryManager.AddItem(item);
                // After adding the item to the inventory, remove it from the scene
                Destroy(item); // Use this to remove the object from the scene
                Debug.Log("Item collected: " + item.name);
            }
            else
            {
                Debug.LogError("InventoryManager is not assigned!");
            }
        }
        else
        {
            Debug.Log("Clicked object is not a collectible: " + item.name);
        }
    }

    // Display the note if the clicked object is a note
    void DisplayNoteIfApplicable(GameObject item)
    {
        if (item.CompareTag("Note")) // Check if the clicked item is a note
        {
            NoteDisplay noteDisplay = item.GetComponent<NoteDisplay>();
            if (noteDisplay != null)
            {
                noteDisplay.ShowNote(); // Show the note on the UI
            }
            else
            {
                Debug.LogError("The object does not have a NoteDisplay component!");
            }
        }
    }

    // Trigger dialogue if the clicked object is an NPC
    void TriggerDialogueOfNPC(GameObject item)
    {
        if (item.CompareTag("NPC")) // Ensure you're checking for the correct tag
        {
            Debug.Log("Detected NPC: " + item.name); // Log NPC detection
            InteractiveChoices interactiveChoices = item.GetComponent<InteractiveChoices>();
            if (interactiveChoices != null)
            {
                interactiveChoices.StartDialogue(); // Call the StartDialogue method
                Debug.Log("NPC interaction triggered.");
            }
            else
            {
                Debug.LogError("The NPC does not have an InteractiveChoices component!");
            }
        }
    }
}
