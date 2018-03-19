using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
	protected InventoryManager() { } // guarantee this will be always a singleton only - can't use the constructor!

	public static InventoryManager Instance = null;

	[SerializeField]
	public Dictionary<ItemType, int> inventory;							// The actual status of the players inventory, how much of what he has
	[SerializeField]

	public List<Image> inventoryIcons;

	public List<InventoryItem> availableItems;


	void Awake()
	{
		// Singleton pattern
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
	}

	void Start()
	{
		inventory = new Dictionary<ItemType, int>();
	}


	/// <summary>
	///  This function gets called when the player interacts with items on the ground, 
	/// or containers containing items that the player can take
	/// The function adds them to the inventory dictionary
	/// </summary>
	/// <param name="type">The type of item that is getting added</param>
	/// <param name="count">The amount of item to be added; e.g. 5 wood</param>
	public void AddItem(ItemType type, int count)
	{
		if (inventory.ContainsKey(type) == true)
		{
			inventory[type] += count;
			UpdateUIElement(type, count);
		}
		else 
		{
			inventory.Add(type, count);
			AddUIElement(type, count);
		}

		print ("I have " + inventory[type] + " " + type);
	}

	public void UpdateUIElement(ItemType type, int count)
	{
		// TODO: This function gets called when the player picks up an item that he already has in his inventory, or when 
		// he gives some of his items away (like giving the lumberjack an axe)
		// so all that happens is that the counter for that item now increases/decreases.
		// For this to work, we need to set this whole thing up different in some ways 
		// to accomodate for updating(we dont know which item is in which inventory slot, so its hard to update it?!)
	}

	public void AddUIElement(ItemType type, int count)
	{

		Sprite newSprite = null;
		for (int i = 0; i < availableItems.Count; i++)
		{
			if (type == availableItems[i].type)
			{
				newSprite = availableItems[i].item;
				break;
			}
		}

		for (int i = 0; i < inventoryIcons.Count; i++)
		{
			if (inventoryIcons[i].sprite == null && newSprite != null && inventoryIcons[i].enabled == false)
			{	
				inventoryIcons[i].enabled = true;
				// Display the sprite in the UI
				inventoryIcons[i].sprite = newSprite;

				// Display the text indicator for the amount you own
				TextMeshProUGUI uiText = inventoryIcons[i].GetComponentInChildren<TextMeshProUGUI>();
				uiText.SetText("x" + count.ToString());
				return;
			}
		}
	}
}