using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

/// <summary>
/// /// This class represents the items in the world
/// 
/// </summary>
public class Item : InteractableBase
{
    [Header("Container Info")]
    [SerializeField, Tooltip("The type of resources stored in this container. There can only be one type per container..")]
    private ItemType type;
    [SerializeField]
    private int count;

    [YarnCommand("pickup")]
    public void pickup(string item)
    {
        // The function call could like something like this:
        InventoryManager.Instance.AddItem(type, count);
        Destroy(gameObject);
    }
}
