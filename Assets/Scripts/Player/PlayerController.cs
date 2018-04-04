using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;
using Rewired;
using Yarn.Unity;

public class PlayerController : MonoBehaviour
{
    #region inspector variables
    [Header("Movement Settings")]
    [SerializeField, Tooltip("The amount of gravity to be applied")]
    private float gravity = -20.0f;
    [SerializeField]
    private float walkSpeed = 2.5f;
    [SerializeField]
    private float sprintSpeed = 4.5f;
    [SerializeField]
    private float ladderClimbSpeed = 1.5f;
    [SerializeField, Tooltip("This is the height of the player jump in unity units")]
    private float jumpHeight = 1.0f;

    #endregion

    #region private variables

    /// <summary>
    /// This class holds all information about how our player is moving in a single frame
    /// </summary>
    private class PlayerMovementState
    {
        public float moveHorizontal;
        public float moveVertical;
        public bool isJumping;
        public bool jumpReleased;
        public bool isSprinting;
        public bool isInteracting;

        public void Reset()
        {
            moveHorizontal = moveVertical = 0.0f;
            isJumping = isSprinting = isInteracting = jumpReleased = false;
        }

        public override string ToString()
        {
            return string.Format("[MovementState] moveHorizontal: {0}, moveVertical: {1}, isJumping: {2}, jumpReleased: {3}, isSprinting: {4}, isInteracting: {5}",
                                 moveHorizontal, moveVertical, isJumping, jumpReleased, isSprinting, isInteracting);
        }
    }

    private int rewiredPlayerId = 0;
    private Player rewiredPlayer;
    private CharacterController2D controller;
    private PlayerMovementState movementState = new PlayerMovementState();
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private InteractableBase interactable;
    private Vector3 velocity = Vector3.zero;
    private DialogueRunner dialogueRunner;
    private Ladder ladder;
    private bool isClimbingLadder = false;

    #endregion

    void Awake()
    {
        rewiredPlayer = ReInput.players.GetPlayer(rewiredPlayerId);
        controller = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
	{
        dialogueRunner = FindObjectOfType<DialogueRunner>();
    }

    void Update()
    {
        // Remove all player control when we're in dialogue
        if (dialogueRunner.isDialogueRunning == true || GameManager.Instance.playerFrozen == true)
        {
            return;
        }

        // grab our current velocity to use as a base for all calculations
        SetInitialVelocity();

        // Get input and process it
        GetInput();
        ProcessInput();

        // Apply gravity to player and then actually move the player along it's velocity vector
        ApplyGravity();
        Move();

        // Flip the sprite in the right direction and set the animation state
        FlipSprite();
        Animate();
    }

    private void SetInitialVelocity()
    {
        velocity = controller.velocity;
        if (controller.isGrounded)
        {
            velocity.y = 0;
        }
    }

    private void GetInput()
    {
        movementState.Reset();

        movementState.moveHorizontal = rewiredPlayer.GetAxis("MoveHorizontal");
        movementState.moveVertical = rewiredPlayer.GetAxis("MoveVertical");
        movementState.isJumping = rewiredPlayer.GetButtonDown("Jump");
        movementState.jumpReleased = rewiredPlayer.GetButtonUp("Jump");
        movementState.isSprinting = rewiredPlayer.GetButton("Sprint");
        movementState.isInteracting = rewiredPlayer.GetButtonDown("Interact");
    }

    private void ProcessInput()
    {
        //// Walk or sprint
        MoveHorizontal();

        //// Climb Ladder
        ClimbLadder();

        //// Jumping
        if (movementState.isJumping && controller.isGrounded)
        {
            Jump();
        }
        else if (movementState.jumpReleased && !controller.isGrounded && velocity.y > 0.0f)
        {
            velocity.y *= 0.5f;
        }

        //// Interacting
        if (movementState.isInteracting)
        {
            Interact();
        }
    }

    private void MoveHorizontal()
    {
        velocity.x = movementState.moveHorizontal;
        velocity.x *= (movementState.isSprinting == true) ? sprintSpeed : walkSpeed;
    }

    private void Jump()
    {
        //// Jumping down through platforms
        // If pressing down turn off one way platform detection for a frame and set velocity.y to a negative number so we can jump down through one way platforms
        if (movementState.moveVertical == -1.0f)
        {
            velocity.y = gravity * Time.deltaTime;
            controller.ignoreOneWayPlatformsThisFrame = true;
        }
        //// Jump normally
        else
        {
            velocity.y = Mathf.Sqrt(2.0f * jumpHeight * -gravity);
        }
    }

    private void ClimbLadder()
    {
        // Early out if we are not near a ladder
        if (ladder == null) return;

        // Grab the ladder
        if (Mathf.Abs(movementState.moveVertical) != 0.0f && !isClimbingLadder)
        {
            isClimbingLadder = true;
        }

        // Only execute this code if player is currently climbing ladder
        if (isClimbingLadder)
        {
            // Move player up/down
            if (movementState.moveVertical != 0.0f)
            {
                velocity.y = movementState.moveVertical;
                velocity.y *= (movementState.isSprinting == true) ? walkSpeed : ladderClimbSpeed;
            }

            // Let go of ladder
            if (movementState.isInteracting || controller.isGrounded)
            {
                isClimbingLadder = false;
            }

            // Jump off of ladder
            if (movementState.isJumping)
            {
                isClimbingLadder = false;
                Jump();
            }
        }
    }

    private void ApplyGravity()
    {
        // Early out if climbing ladder
        if (isClimbingLadder) return;

        velocity.y += (gravity * Time.deltaTime);
    }

    private void Move()
    {
        // Move the player along it's velocity vector
        controller.move(velocity * Time.deltaTime);
    }

    private void FlipSprite()
    {
        // This assumes that flipX is true when character is facing left
        // Sprite is flipped if flipX is true (sprite is facing left) and the character is moving right
        // or the other way around.
        if ((spriteRenderer.flipX && velocity.x > 0) || (!spriteRenderer.flipX && velocity.x < 0))
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    private void Animate()
    {
        animator.SetBool("movingHorizontal", velocity.x != 0.0f);
        animator.SetBool("isGrounded", controller.isGrounded);

        if (animator.GetBool("movingHorizontal") && !animator.GetBool("onLadder") && !AudioManager.instance.IsPlaying("PlayerWalk") && controller.isGrounded)
        {
            AudioManager.instance.Play("PlayerWalk");
        }

        animator.SetBool("onLadder", isClimbingLadder);
        animator.SetBool("isSprinting", movementState.isSprinting);
        animator.SetFloat("yVelocity", velocity.y);

        if (isClimbingLadder)
        {
            // FIXME: This is a really hacky way to tweak the animation speed when climbing ladder

            float animSpeed = new Vector2(movementState.moveHorizontal, movementState.moveVertical).normalized.magnitude;
            if (movementState.isSprinting)
            {
                animSpeed *= 1.2f;
            }
            animator.speed = Mathf.Abs(animSpeed);

        }
        else
        {
            animator.speed = 1;
        }

    }

    public void SetInteractable(InteractableBase newInteractable)
    {
        interactable = newInteractable;
    }

    public void SetLadder(Ladder stairs)
    {
        this.ladder = stairs;

        if (stairs == null)
        {
            isClimbingLadder = false;
        }
    }

    /// <summary>
    /// This function is called when the player presses the interact button
    /// If the player is standing close to an interactable object, he will
    /// call the interact function of that object
    /// </summary>
    public void Interact()
    {
        if (interactable != null && controller.isGrounded == true)
        {
            interactable.Interact();
            velocity.x = 0;
        }
    }
}
