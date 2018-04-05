using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Woodsman : NPC
{
    [Header("Woodsman variables")]
    [SerializeField]
    private GameObject woodContainer;

    [SerializeField]
    private const float addWoodInterval = 2.5f;

    [SerializeField]
    private Sprite happyAvatar;


    private bool hasWholeAxe = false;
    private float chopTimer = 0.0f;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (hasWholeAxe == true)
        {
            chopTimer += Time.deltaTime;
            if (chopTimer >= addWoodInterval)
            {
                woodContainer.GetComponent<Container>().AddItem(ItemType.Wood, 20);
                chopTimer = 0.0f;

                string variableName = "$woodcontainer_amount";
                FindObjectOfType<VariableStorage>().SetValue(variableName, new Yarn.Value(woodContainer.GetComponent<Container>().GetCount()));
            }

            if (Random.Range(0f, 1f) < 0.001f)
            {
                animator.SetTrigger("isSweating");
            }
        }

        if (isFacingRight)
        {
            if (GameManager.Instance.Player.transform.position.x > transform.position.x && spriteRenderer.flipX ||
                GameManager.Instance.Player.transform.position.x < transform.position.x && !spriteRenderer.flipX)
            {
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }
        }
        else
        {
            if (GameManager.Instance.Player.transform.position.x > transform.position.x && !spriteRenderer.flipX ||
                GameManager.Instance.Player.transform.position.x < transform.position.x && spriteRenderer.flipX)
            {
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }
        }
    }

    [YarnCommand("chop")]
    public void StartChopping()
    {
        hasWholeAxe = true;
        animator.SetBool("isChopping", true);
    }

    [YarnCommand("set_happy")]
    public void SetHappy()
    {
        if (happyAvatar == null)
        {
            Debug.LogError("Missing Happy Avatar image on " + gameObject.name + " - in the Woodsman script");
        }
        else
        {
            GetComponent<CanSpeak>().SetNewAvatar(happyAvatar);
        }
    }
}
