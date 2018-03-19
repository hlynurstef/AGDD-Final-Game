using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : PhysicsObject {

	public float maxSpeed = 7;
	public float jumpTakeOffSpeed = 7;

	private bool right = true;

	//private SpriteRenderer spriteRenderer;
	private Animator animator;
	[SerializeField]
	private IInteractable interactable;

	// Use this for initialization
	void Awake () 
	{
		//spriteRenderer = GetComponent<SpriteRenderer> (); 
		animator = GetComponent<Animator> ();
	}

	void Start()
	{
		interactable = null;
	}

	protected override void ComputeVelocity()
	{
		Vector2 move = Vector2.zero;

		move.x = Input.GetAxis ("Horizontal");

		if (move.x != 0f) {
			animator.SetBool ("isWalking", true);
		} else {
			animator.SetBool("isWalking", false);
		}

		if (Input.GetButtonDown ("Jump") && grounded) {
			velocity.y = jumpTakeOffSpeed;
		} else if (Input.GetButtonUp ("Jump")) 
		{
			if (velocity.y > 0) {
				velocity.y = velocity.y * 0.5f;
			}
		}

		if (Input.GetButtonDown("Interact") == true)
		{
			Interact();
		}

		//bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
		if (move.x > 0.0f && !right) {
			//spriteRenderer.flipX = !spriteRenderer.flipX;
			right = true;
			transform.Rotate(0,180,0);

		} else if (move.x < 0.0f && right) {
			right = false;
			transform.Rotate(0,180,0);
		}

	//	animator.SetBool ("grounded", grounded);
	//	animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxSpeed);


		targetVelocity = move * maxSpeed;
	}

	public void SetInteractable(IInteractable newInteractable)
	{
		interactable = newInteractable;
	}

	/// <summary>
	/// This function is called when the player presses the interact button
	/// If the player is standing close to an interactable objet, he will
	/// call the interact function of that object
	/// </summary>
	public void Interact()
	{
		if (interactable != null)
		{
			interactable.Interact();
		}
	}
}