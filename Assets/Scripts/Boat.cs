using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Boat : MonoBehaviour 
{
	public Transform leftSide;
	public Transform rightSide;

	public Transform currentSide;
	public Transform boatTransform;

	private void Start()
	{
		currentSide = leftSide;
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
			mySeq.OnComplete(() => { currentSide = targetSide; });
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
