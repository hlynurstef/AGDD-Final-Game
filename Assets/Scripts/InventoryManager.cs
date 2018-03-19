using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	protected InventoryManager() { } // guarantee this will be always a singleton only - can't use the constructor!

	public static InventoryManager Instance = null;

	[SerializeField]
	public Dictionary<ItemType, int> inventory;							// The actual status of the players inventory, how much of what he has
	[SerializeField]

	public List<SpriteRenderer> inventoryIcons;

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
		
		// TODO: Include this if the game will be built in multiple scenes
		//DontDestroyOnLoad(gameObject);

		// TODO: The inventory UI has to get it's information from this script. It should read the dictionary and fill in the 
		// inventory slots with the right type/amount for each of the items.
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
		// TODO: Remove print statement when we are sure everything works as intended

		

		print ("I have " + inventory[type] + " " + type);
	}

	public void UpdateUIElement(ItemType type, int count)
	{

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
			if (inventoryIcons[i].sprite == null && newSprite != null)
			{
				inventoryIcons[i].sprite = newSprite;
				return;
			}
		}
	}
}