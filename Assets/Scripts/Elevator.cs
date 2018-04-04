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
			Debug.Log("We chose yes!");
            elevatorAnimator.SetBool("IsDownstairs", true);
        }
		else if (direction.ToLower().Equals("down"))
        {
            elevatorAnimator.SetBool("IsDownstairs", false);
        }
    }
}
