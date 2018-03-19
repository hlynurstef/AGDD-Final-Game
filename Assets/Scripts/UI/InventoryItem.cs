using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InventoryItem : ScriptableObject {

	public Sprite item;
	public ItemType type;
	public string itemName;
}
