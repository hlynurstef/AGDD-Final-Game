using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Lever : InteractableBase
{
	[SerializeField]
	private Sprite[] sprites;
	private SpriteRenderer spriteRenderer;
	private int currentSpriteIndex;
	

	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		currentSpriteIndex = 0;
	}

	[YarnCommand("flip")]
	public void FlipLever()
	{
		print("Flipping");
		currentSpriteIndex = (currentSpriteIndex == 1) ? 0 : 1;
		spriteRenderer.sprite = sprites[currentSpriteIndex];
	}
}
