using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using DG.Tweening;

public class Elevator : InteractableBase {

	public Transform elevatorTransform;
	public Transform upstairsCheckpoint;
	public Transform downstairsCheckpoint;

	[YarnCommand("move")]
    public void Move(string direction)
    {

		GameObject player = GameManager.Instance.Player;
		// Freeze player movement
		GameManager.Instance.playerFrozen = true;
		// Tween boat from one side to the other
		_interactIcon.SetActive(false);

		Sequence mySeq = DOTween.Sequence();
		

        if (direction.ToLower().Equals("up"))
        {
			Vector3 playerTarget = new Vector3(upstairsCheckpoint.position.x, upstairsCheckpoint.position.y + 1, upstairsCheckpoint.position.z);
			mySeq.Append(elevatorTransform.DOMove(upstairsCheckpoint.position, 5, false));
			mySeq.Insert(0, player.transform.DOMove(playerTarget, 5, false));
			mySeq.PrependInterval(1.0f);
			mySeq.OnComplete(() => { 
				GameManager.Instance.playerFrozen = false;
				_interactIcon.SetActive(true);
			});
            
        }
		else if (direction.ToLower().Equals("down"))
        {
			Vector3 playerTarget = new Vector3(downstairsCheckpoint.position.x, downstairsCheckpoint.position.y + 1, downstairsCheckpoint.position.z);
			mySeq.Append(elevatorTransform.DOMove(downstairsCheckpoint.position, 5, false));
			mySeq.Insert(0, player.transform.DOMove(playerTarget, 5, false));
			mySeq.PrependInterval(1.0f);
			mySeq.OnComplete(() => { 
				GameManager.Instance.playerFrozen = false;
				_interactIcon.SetActive(true);
			});
        }
    }
}
