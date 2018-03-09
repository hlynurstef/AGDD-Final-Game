using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour {

	public InventoryItem inventoryItem;

	public SpriteRenderer item;

	// Use this for initialization
	void Start () {
		item.sprite = inventoryItem.item;
	}
}
