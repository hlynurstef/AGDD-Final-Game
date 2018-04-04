using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Yarn.Unity;

/// <summary>
/// This class represents the containers in the world
/// These are for example the container that the forester uses to store his wood in,
/// the cart that the miner stores his ore in and more
/// </summary>
public class Diamonds : InteractableBase
{
    private ItemType type = ItemType.Diamond;
    private int count = 1;

    [YarnCommand("mine")]
    public void Mine()
    {
        // The function call could like something like this:
        InventoryManager.Instance.AddItem(type, count);
        count = 0;

        string variableName = "$" + type.ToString().ToLower() + "container" + "_amount";
        FindObjectOfType<VariableStorage>().SetValue(variableName, new Yarn.Value(0));
    }

    [YarnCommand("break")]
    public void BreakPickaxe()
    {
        InventoryManager.Instance.RemoveItem(ItemType.Pickaxe, 1);
    }
}
