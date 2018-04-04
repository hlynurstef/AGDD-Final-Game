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
    private const float addWoodInterval = 120.0f;

    [SerializeField]
    private Sprite happyAvatar;


    private bool hasWholeAxe = false;
    private float chopTimer = 0.0f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (hasWholeAxe == true)
        {
            chopTimer += Time.deltaTime;
            if (chopTimer >= addWoodInterval)
            {
                woodContainer.GetComponent<Container>().AddItem(ItemType.Wood, 5);
                chopTimer = 0.0f;

                string variableName = "$woodcontainer_amount";
                FindObjectOfType<VariableStorage>().SetValue(variableName, new Yarn.Value(woodContainer.GetComponent<Container>().GetCount()));
            }

            if (Random.Range(0f, 1f) < 0.001f)
            {
                animator.SetTrigger("isSweating");
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
