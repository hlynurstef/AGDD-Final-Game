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
public class Container : InteractableBase
{
    [Header("Container Info")]
    [SerializeField, Tooltip("The amount of resources inside the container at any given time")]
    private int count;
    [SerializeField, Tooltip("The type of resources stored in this container. There can only be one type per container..")]
    private ItemType type;

    private int capacity = 15;  // The maximum amount of resources this container can carry, possibly upgradable by talking to people

    /// <summary>
    /// Should get called when, for example, the lumberjack is adding wood to the container
    /// You can only add wood to a container that is set to accept wood in the inspector.
    /// This is done to prevent bugs like the miner adding ore to the lumberjacks container and such
    /// </summary>
    /// <param name="type">The type of item to be added to the container, e.g. the lumberjack is adding wood to it</param>
    /// <param name="count">The number of items to add to the container, this could increase when the lumberjack has a new axe</param>
    public void AddItem(ItemType matType, int matCount)
    {
        if (type == matType && count < capacity)
        {
            count += matCount;
            count = (count <= capacity) ? count : capacity;
        }
    }

    [YarnCommand("take")]
    public void Take(string amount)
    {
        if (amount.ToLower().Equals("all"))
        {
            // The function call could like something like this:
            InventoryManager.Instance.AddItem(type, count);
            count = 0;

            string variableName = "$" + type.ToString().ToLower() + "container" + "_amount";
            FindObjectOfType<VariableStorage>().SetValue(variableName, new Yarn.Value(0));
        }
    }

    public int GetCount()
    {
        return count;
    }
}
