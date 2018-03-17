using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour, IInteractable
{
	public int count;							// The amount of resources inside the container at any given time
	public MaterialType type;					// The type of resources stored in this container. There can only be one type per container. 
	private int capacity = 15;					// The maximum amount of resources this container can carry, possibly upgradable by talking to people



	void Update()
	{
		// TODO: Check if count is > 0. If so, display particle effect to indicate that the player can interact with box? 
	}


	/// <summary>
	/// Should get called when, for example, the lumberjack is adding wood to the container
	/// </summary>
	/// <param name="type">The type of item to be added to the container, e.g. the lumberjack is adding wood to it</param>
	/// <param name="count">The number of items to add to the container, this could increase when the lumberjack has a new axe</param>
	public void AddItem(MaterialType matType, int matCount)
	{
		if (type == matType && count < capacity)
		{
			count += matCount;
			count = (count <= capacity) ? count : capacity;
		}
	}

	public void Interact()
	{
		print ("This container has: " + count + " "+ type);
		// TODO: Talk to the inventory manager, let him know you now have a bunch of new stuff like wood and sjitz
		// The function call could like something like this:
		// InventoryManager.Instance.AddMaterial(count, type);
		count = 0;
	}

	public MaterialType GetContainerType()
	{
		return type;
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player") == true)
		{
			// TODO: Show interact button icon here
			other.gameObject.GetComponent<PlayerController2D>().SetInteractableContainer(this);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player") == true)
		{
			// TODO: Hide interact button icon here
			other.gameObject.GetComponent<PlayerController2D>().SetInteractableContainer(null);
		}
	}
}
