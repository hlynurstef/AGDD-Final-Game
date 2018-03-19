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
    private float jumpHeight = 3.0f;

    private Player rewiredPlayer;
    private CharacterController2D controller;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private IInteractable interactable;
    private Vector3 velocity = Vector3.zero;

    void Awake()
    {
        rewiredPlayer = ReInput.players.GetPlayer(rewiredPlayerId);
        controller = GetComponent<CharacterController2D>();
        //animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Remove all player control when we're in dialogue
        if (FindObjectOfType<DialogueRunner>().isDialogueRunning == true)
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
    }

    private void GetInput()
    {
        velocity.x = rewiredPlayer.GetAxis("MoveHorizontal") * runSpeed;
        // TODO: implement better jumping ( https://www.youtube.com/watch?v=hG9SzQxaCm8 )
        velocity.y = rewiredPlayer.GetButtonDown("Jump") && controller.isGrounded ? Mathf.Sqrt(2.0f * jumpHeight * -gravity) : velocity.y;

        if (rewiredPlayer.GetButtonDown("Interact"))
        {
            Interact();
        }
    }

    private void Move()
    {
        velocity.y += gravity * Time.deltaTime;

        velocity *= Time.deltaTime;

        controller.move(velocity);
    }

    private void FlipSprite()
    {
        // This assumes that flipX is true when character is moving left
        // Sprite is flipped if flipX is true (sprite is facing left) and the character is moving right
        // or the other way around.
        if ((spriteRenderer.flipX && velocity.x > 0) || (!spriteRenderer.flipX && velocity.x < 0))
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
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
