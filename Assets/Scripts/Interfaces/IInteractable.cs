using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
	/// <summary>
	/// This should handle the interaction that happens
	/// when the player presses the interact button while standing close to the interactable
	/// This should be used for NPC's and containers
	/// </summary>
	void Interact();
}
