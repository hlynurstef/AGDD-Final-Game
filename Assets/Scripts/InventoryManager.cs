using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	protected InventoryManager() { } // guarantee this will be always a singleton only - can't use the constructor!

	public static InventoryManager instance = null;

	void Awake()
	{
		// Singleton pattern
		if (instance != null)
		{
			Destroy(gameObject);
			return;
		}

		instance = this;
		DontDestroyOnLoad(gameObject);
	}

	
}