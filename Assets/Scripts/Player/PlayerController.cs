using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;
using Rewired;
using Yarn.Unity;

public class PlayerController : MonoBehaviour
{
    [Header("Rewired settings")]
    [SerializeField]
    private int rewiredPlayerId;

    [Header("Movement Settings")]
    [SerializeField]
    private float gravity = -15.0f;
    [SerializeField]
    private float runSpeed = 8.0f;
    [SerializeField]
    private float sprintSpeed = 8.010f;
    [SerializeField]
    private float jumpHeight = 3.0f;
    [SerializeField]
    private float fallMultiplier = 2.5f;
    [SerializeField]
    private float lowJumpMultiplier = 2.0f;

    private Player rewiredPlayer;
    private CharacterController2D controller;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private InteractableBase interactable;
    private Vector3 velocity = Vector3.zero;
    private DialogueRunner dialogueRunner;
    private Ladder ladder;
    private bool isClimbingLadder = false;

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
        velocity = controller.velocity;
        if (controller.isGrounded)
        {
            velocity.y = 0;
        }

        GetInput();
        Move();
        FlipSprite();
        Animate();
    }

    private void GetInput()
    {
        velocity.x = rewiredPlayer.GetAxis("MoveHorizontal");
        velocity.x *= (rewiredPlayer.GetButton("Sprint") == true) ? sprintSpeed : runSpeed;
        // TODO: implement better jumping ( https://www.youtube.com/watch?v=hG9SzQxaCm8 )
        velocity.y = rewiredPlayer.GetButtonDown("Jump") && controller.isGrounded ? Mathf.Sqrt(2.0f * jumpHeight * -gravity) : velocity.y;
        // velocity.y = rewiredPlayer.GetButtonDown("Jump") && controller.isGrounded ? jumpHeight : velocity.y;

        // if holding down we turn off one way platform detection for a frame and set velocity.y to a negative number
        // this lets us jump down through one way platforms
        if (rewiredPlayer.GetButton("Jump") && controller.isGrounded && rewiredPlayer.GetAxisRaw("MoveVertical") == -1)
        {
            velocity.y = -0.1f;
            controller.ignoreOneWayPlatformsThisFrame = true;
        }

        if (rewiredPlayer.GetButtonDown("Interact"))
        {
            Interact();
        }

        if (rewiredPlayer.GetAxisRaw("MoveVertical") == 1.0f ||
            rewiredPlayer.GetAxisRaw("MoveVertical") == -1.0f)
        {
            // Grab the ladder if able
            if (!isClimbingLadder && ladder != null)
            {
                isClimbingLadder = true;
            }
        }

        // Only execute this code if player is currently climbing ladder
        if (isClimbingLadder)
        {
            if (rewiredPlayer.GetAxis("MoveVertical") != 0.0f)
            {
                velocity.y = rewiredPlayer.GetAxis("MoveVertical");
                velocity.y *= (rewiredPlayer.GetButton("Sprint") == true) ? sprintSpeed : runSpeed;
            }

            // Let go of ladder
            if (rewiredPlayer.GetButtonDown("Interact") ||
                rewiredPlayer.GetButtonDown("Jump") ||
                controller.isGrounded)
            {
                isClimbingLadder = false;
            }
        }
    }

    private void Move()
    {
        // Only apply gravity if not on ladder
        if (!isClimbingLadder)
        {
            velocity.y += gravity * Time.deltaTime;
            if (velocity.y < 0)
            {
                velocity.y += gravity * fallMultiplier * Time.deltaTime;
            }
            else if (velocity.y > 0 && !rewiredPlayer.GetButton("Jump"))
            {
                velocity.y += gravity * lowJumpMultiplier * Time.deltaTime;
            }
        }


        velocity *= Time.deltaTime;

        controller.move(velocity);
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
        animator.SetBool("isWalking", velocity.x != 0.0f);
        animator.SetBool("onLadder", isClimbingLadder);

        if (isClimbingLadder)
        {
            // FIXME: This is a really hacky way to tweak the animation speed when climbing ladder
            animator.speed = Mathf.Abs(velocity.y / (runSpeed * Time.deltaTime));

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
    /// If the player is standing close to an interactable objet, he will
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
