using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	protected InventoryManager() { } // guarantee this will be always a singleton only - can't use the constructor!

	public static InventoryManager Instance = null;

	public Dictionary<ItemType, int> inventory;

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
	}

	void Start()
	{
		inventory = new Dictionary<ItemType, int>();
	}


	public void AddMaterial(ItemType type, int count)
	{
		if (inventory.ContainsKey(type) == true)
		{
			inventory[type] += count;
		}
		else 
		{
			inventory.Add(type, count);
		}

		print ("I have " + inventory[type] + " many " + type);
	}
	
}