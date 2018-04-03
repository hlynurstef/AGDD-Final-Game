using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using DG.Tweening;
public class Boat : InteractableBase 
{
	[SerializeField]
	private Transform leftSide;
	[SerializeField]
	private Transform rightSide;
	[SerializeField]
	private Transform currentSide;
	[SerializeField]
	private Transform boatTransform;
	[SerializeField]
	private Collider2D leftCollider;
	[SerializeField]
	private Collider2D rightCollider;

	private void Start()
	{
		currentSide = leftSide;
		leftCollider.enabled = (currentSide == leftSide) ? false : true;
		rightCollider.enabled = (currentSide == rightSide) ? false : true;
	}

	[YarnCommand("sail")]
    public void Sail(string okay)
    {
		GameObject player = GameManager.Instance.Player;
		
		// Enable colliders on both sides of boat to prevent player from walking into the water
		leftCollider.enabled = true;
		rightCollider.enabled = true;

		// Set the boat as parent to the player so player moves with the boat
		player.transform.parent = boatTransform;
		player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;

		// Tween boat from one side to the other
		Sequence mySeq = DOTween.Sequence();
		Transform targetSide = (currentSide == rightSide) ? leftSide : rightSide;
		mySeq.Append(boatTransform.DOMove(targetSide.position, 10, false));
		mySeq.PrependInterval(1.0f);
		mySeq.OnComplete(() => { 
			currentSide = targetSide;
			// Disable the collider facing the dock so the player can leave the boat 
			leftCollider.enabled = (currentSide == leftSide) ? false : true;
			rightCollider.enabled = (currentSide == rightSide) ? false : true;
			player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
			player.transform.parent = null;
		});
    }

}
