﻿using System.Collections;
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

	// TODO: Do something different
	public Dictionary<ItemType, int> uiIndices;


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
		uiIndices = new Dictionary<ItemType, int>();
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


	/// <summary>
	/// This function removes count many items of type "type"
	/// </summary>
	/// <param name="type">The type of item to be removed</param>
	/// <param name="count">The amount of the item to remove</param>
	/// <returns></returns>
	public bool RemoveItem(ItemType type, int count)
	{
		if (inventory.ContainsKey(type) == true)
		{
			if (inventory[type] >= count)
			{
				inventory[type] -= count;
				if (inventory[type] <= 0)
				{
					// TODO: Remove the icon from the inventory slot!
				}
			}
			else
			{
				return false;
			}
		}
		return false;
	}

	public void UpdateUIElement(ItemType type, int count)
	{
		// TODO: This function gets called when the player picks up an item that he already has in his inventory, or when 
		// he gives some of his items away (like giving the lumberjack an axe)
		// so all that happens is that the counter for that item now increases/decreases.
		// For this to work, we need to set this whole thing up different in some ways 
		// to accomodate for updating(we dont know which item is in which inventory slot, so its hard to update it?!)
		int index;
		if (uiIndices.ContainsKey(type) == true)
		{
			index = uiIndices[type];
			TextMeshProUGUI uiText = inventoryIcons[index].GetComponentInChildren<TextMeshProUGUI>();
			int total = inventory[type];
			uiText.SetText("x" + total.ToString());
		}
	}

	public void AddUIElement(ItemType type, int count)
	{
		if (count <= 0)
		{	
			// Dont add a new UI element if the amount we have is zero or less
			return;
		}
		Sprite newSprite = null;
		for (int i = 0; i < availableItems.Count; i++)
		{
			if (type == availableItems[i].type)
			{
				newSprite = availableItems[i].item;
				break;
			}
		}

		int index;
		for (index = 0; index < inventoryIcons.Count; index++)
		{
			if (inventoryIcons[index].sprite == null && newSprite != null && inventoryIcons[index].enabled == false)
			{	
				inventoryIcons[index].enabled = true;
				// Display the sprite in the UI
				inventoryIcons[index].sprite = newSprite;

				// Display the text indicator for the amount you own
				TextMeshProUGUI uiText = inventoryIcons[index].GetComponentInChildren<TextMeshProUGUI>();
				uiText.SetText("x" + count.ToString());
				break;
			}
		}

		if (uiIndices.ContainsKey(type) == false)
		{
			uiIndices.Add(type, index);
		}
	}
}