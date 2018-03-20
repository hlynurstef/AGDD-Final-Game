using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class NPC : MonoBehaviour, IInteractable
{

    public GameObject interactIcon;
    [SerializeField, Tooltip("The Yarn node to play when talking to this NPC")]
    private string talkToNode = "";

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        if (interactIcon != null)
        {
            interactIcon.SetActive(false);
        }
    }

    /// <summary>
    /// The NPC's interact function. It should open a chat window with the NPC that the player is talking to
    /// </summary>
    public void Interact()
    {
        // Start dialog if talkToNode is not null or empty
        if (string.IsNullOrEmpty(talkToNode) == false)
        {
            FindObjectOfType<DialogueRunner>().StartDialogue(talkToNode);
        }
    }

	[YarnCommand("take")]
	public void TakePlayerItems(string type, string count)
	{
		ItemType concreteType;
		int concreteCount = int.Parse(count);
		foreach(ItemType itemType in ItemType.GetValues(typeof(ItemType)))
		{
			if (type == itemType.ToString())
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
		foreach(ItemType itemType in ItemType.GetValues(typeof(ItemType)))
		{
			if (type == itemType.ToString())
			{
				concreteType = itemType;
				InventoryManager.Instance.AddItem(concreteType, concreteCount);
				return;
			}
		}
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") == true)
        {
            if (interactIcon != null)
            {
                interactIcon.SetActive(true);
            }
            other.gameObject.GetComponent<PlayerController>().SetInteractable(this);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") == true)
        {
            if (interactIcon != null)
            {
                interactIcon.SetActive(false);
            }
            other.gameObject.GetComponent<PlayerController>().SetInteractable(null);
        }
    }
}
