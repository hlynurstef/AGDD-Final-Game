using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Boat : MonoBehaviour 
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
	private void OnTriggerEnter2D(Collider2D other) 
	{
		// TODO: Disable player controls while boat is transporting the player?
		if (other.gameObject.CompareTag("Player") == true)
		{
			other.gameObject.transform.parent = boatTransform;
			Sequence mySeq = DOTween.Sequence();
			Transform targetSide = (currentSide == rightSide) ? leftSide : rightSide;
			mySeq.Append(boatTransform.DOMove(targetSide.position, 10, false));
			mySeq.PrependInterval(1.0f);
			mySeq.OnComplete(() => { 
				currentSide = targetSide; 
				leftCollider.enabled = (currentSide == leftSide) ? false : true;
				rightCollider.enabled = (currentSide == rightSide) ? false : true;
			});
		}
	}

	private void OnTriggerExit2D(Collider2D other) 
	{
		if (other.gameObject.CompareTag("Player") == true)
		{
			other.gameObject.transform.parent = null;
		}	
	}

}
