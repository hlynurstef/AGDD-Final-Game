using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour {

	public InventoryItem inventoryItem;

	public Dictionary<ItemType, SpriteRenderer> inventoryIcons;
	public Dictionary<ItemType, int> itemQuantities;

	// Use this for initialization
	void Start () {
		inventoryIcons = new Dictionary<ItemType, SpriteRenderer>();
	}

	public void AddItemToUI(ItemType type, int count)
	{

	}
}
