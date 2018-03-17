using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class represents the containers in the world
/// These are for example the container that the forester uses to store his wood in,
/// the cart that the miner stores his ore in and more
/// </summary>
public class Container : MonoBehaviour, IInteractable
{
	public int count;							// The amount of resources inside the container at any given time
	public ItemType type;					// The type of resources stored in this container. There can only be one type per container. 
	private int capacity = 15;					// The maximum amount of resources this container can carry, possibly upgradable by talking to people



	void Update()
	{
		// TODO: Check if count is > 0. If so, display particle effect to indicate that the player can interact with box? 
	}


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

	/// <summary>
	/// This function is called when the player is standing close to this container and presses the interact button
	/// </summary>
	public void Interact()
	{
		// TODO: Remove print statement when we are sure everything works as intended
		print ("This container has: " + count + " "+ type);
		// TODO: Talk to the inventory manager, let him know you now have a bunch of new stuff like wood and sjitz
		// The function call could like something like this:
		InventoryManager.Instance.AddItem(type, count);
		count = 0;
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player") == true)
		{
			// TODO: Show interact button icon here or do it from the player script ? 
			other.gameObject.GetComponent<PlayerController2D>().SetInteractable(this);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player") == true)
		{
			// TODO: Hide interact button icon here
			other.gameObject.GetComponent<PlayerController2D>().SetInteractable(null);
		}
	}
}
