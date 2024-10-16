using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryButtonPrefab; // Reference to the "Button" prefab
    public Transform inventoryContent;       // The content area where buttons will be added
    private Dictionary<string, int> inventoryItems = new Dictionary<string, int>(); // Store item names and their quantities

    // Add an item to the inventory
    public void AddItem(GameObject item)
    {
        if (inventoryButtonPrefab == null)
        {
            Debug.LogError("Inventory Button Prefab is not assigned!");
            return;
        }
        if (inventoryContent == null)
        {
            Debug.LogError("Inventory Content is not assigned!");
            return;
        }

        string itemName = item.name;

        // If the item already exists in the inventory, increase the quantity
        if (inventoryItems.ContainsKey(itemName))
        {
            inventoryItems[itemName]++;
        }
        else
        {
            inventoryItems[itemName] = 1;

            // Create a button in the inventory to represent the item
            GameObject button = Instantiate(inventoryButtonPrefab, inventoryContent);
            Debug.Log("Button instantiated for item: " + item.name);

            TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = itemName + " x" + inventoryItems[itemName];
                Debug.Log("Button text set to: " + item.name);
            }
            else
            {
                Debug.LogError("No TextMeshProUGUI component found in the 'Button' prefab!");
            }

            Button buttonComponent = button.GetComponent<Button>();
            if (buttonComponent != null)
            {
                buttonComponent.onClick.AddListener(() => UseItem(itemName));
                Debug.Log("Button listener added for item: " + item.name);
            }
            else
            {
                Debug.LogError("No Button component found in the 'Button' prefab!");
            }
        }

        UpdateInventoryUI();
    }


    // Check if any key exists in the inventory
    public bool HasAnyKey()
    {
        return HasItem("Key") || HasItem("Key(Clone)"); // Ensure the exact name matches your keys in inventory
    }

    // Update the text of all items in the inventory to reflect their current quantity
    private void UpdateInventoryUI()
    {
        foreach (Transform child in inventoryContent)
        {
            TextMeshProUGUI buttonText = child.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                string itemName = buttonText.text.Split(' ')[0];
                if (inventoryItems.ContainsKey(itemName))
                {
                    buttonText.text = itemName + " x" + inventoryItems[itemName];
                }
            }
        }
    }

    // Check if an item exists in the inventory by its name and quantity
    public bool HasItem(string itemName, int quantity = 1)
    {
        return inventoryItems.ContainsKey(itemName) && inventoryItems[itemName] >= quantity;
    }

    // Check if any item in the inventory contains the specified substring in its name
    public bool HasItemContaining(string substring)
    {
        foreach (var item in inventoryItems.Keys)
        {
            if (item.Contains(substring))
            {
                return true; // Found an item containing the specified substring
            }
        }
        return false; // No items containing the substring found
    }

    // Remove an item from the inventory by its name
    public void RemoveItem(string itemName, int quantity = 1)
    {
        if (inventoryItems.ContainsKey(itemName))
        {
            inventoryItems[itemName] -= quantity;
            if (inventoryItems[itemName] <= 0)
            {
                inventoryItems.Remove(itemName);
            }

            UpdateInventoryUI();
        }
        else
        {
            Debug.LogError("Attempted to remove item that is not in inventory: " + itemName);
        }
    }

    // Example method for using an item
    public void UseItem(string itemName)
    {
        Debug.Log("Using item: " + itemName);

        // Check if the item is a key and handle the chest interaction
        if (itemName.Contains("Key"))
        {
            // Call the method to open the chest here
            OpenChest();
        }

        // Remove the item after usage
        RemoveItem(itemName);
    }

    // Open the chest when the key is used
    private void OpenChest()
    {
        Debug.Log("Chest opened!");
        // Add your chest opening logic here (e.g., reveal rewards, disable chest UI, etc.)
    }

    // Remove an item that contains the specified substring
    public void RemoveItemContaining(string substring, int quantity = 1)
    {
        foreach (var item in new List<string>(inventoryItems.Keys))
        {
            if (item.Contains(substring))
            {
                RemoveItem(item, quantity);
                return; // Remove only one matching item
            }
        }
        Debug.LogWarning("No item containing '" + substring + "' found in inventory to remove.");
    }
}
