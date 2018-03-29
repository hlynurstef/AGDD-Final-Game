using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InventoryItem : ScriptableObject 
{
	// The items sprite in game
	public Sprite itemSprite;
	// The items sprite in the inventory UI
	public Sprite UISprite;
	public ItemType type;
	public string itemName;
}
