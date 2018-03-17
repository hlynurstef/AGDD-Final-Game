using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An interface that all entities that the player can interact with should implement
/// It forces you to implement the function Interact(), where you can specify
/// what should happen during the interaction (i.e. a chat box could open or the player could be picking up stuff)
/// </summary>
public interface IInteractable
{
	/// <summary>
	/// This should handle the interaction that happens
	/// when the player presses the interact button while standing close to the interactable
	/// This should be implemented for NPC's and containers, and other interactable objects in the world
	/// For an example, see Container.cs 
	/// </summary>
	void Interact();
}
