using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

[RequireComponent(typeof(CanSpeak))]
public class NPC : InteractableBase
{
    public bool isFacingRight = true;

    protected SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
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

    [YarnCommand("take")]
    public void TakePlayerItems(string type, string count)
    {
        ItemType concreteType;
        int concreteCount = int.Parse(count);
        foreach (ItemType itemType in ItemType.GetValues(typeof(ItemType)))
        {
            if (type.ToLower() == itemType.ToString().ToLower())
            {
                concreteType = itemType;
                InventoryManager.Instance.RemoveItem(concreteType, concreteCount);
                return;
            }
        }
    }

    [YarnCommand("give")]
    public void GivePlayerItems(string type, string count)
    {
        ItemType concreteType;
        int concreteCount = int.Parse(count);
        foreach (ItemType itemType in ItemType.GetValues(typeof(ItemType)))
        {
            if (type.ToLower() == itemType.ToString().ToLower())
            {
                concreteType = itemType;
                InventoryManager.Instance.AddItem(concreteType, concreteCount);
                return;
            }
        }
    }
}
