using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Elevator : InteractableBase {

	public Animator elevatorAnimator;
	

	[YarnCommand("move")]
    public void Move(string direction)
    {
        if (direction.ToLower().Equals("up"))
        {
			GameManager.Instance.playerFrozen = true;
            elevatorAnimator.SetBool("IsDownstairs", true);
        }
		else if (direction.ToLower().Equals("down"))
        {
			GameManager.Instance.playerFrozen = true;
            elevatorAnimator.SetBool("IsDownstairs", false);
        }
    }
}
