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
    private IInteractable interactable;
    private Vector3 velocity = Vector3.zero;
    private DialogueRunner dialogueRunner;

    void Awake()
    {
        rewiredPlayer = ReInput.players.GetPlayer(rewiredPlayerId);
        controller = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
    }

    // Update is called once per frame
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

        if (rewiredPlayer.GetButtonDown("Interact"))
        {
            Interact();
        }


    }

    private void Move()
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
        if (interactable != null && controller.isGrounded == true)
        {
            interactable.Interact();
            velocity.x = 0;
        }
    }
}
