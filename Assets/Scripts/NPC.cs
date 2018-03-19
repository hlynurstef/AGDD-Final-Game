using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class NPC : MonoBehaviour, IInteractable
{

    public GameObject interactIcon;
    [SerializeField, Tooltip("The Yarn node associated with this NPC")]
    private string talkToNode = "";
    private bool speaking;

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

        /*
		// TODO: Finish the dialogue system. 
		if (speaking == false) {
			// TODO: Speaking never reset to false? not sure, halp, i dont know how dialogue system wörks
			// TODO: Disable players regular input while talking ? Mayyybe. If so, then the dialogue manager should probably take care of that
			speaking = true;
			GetComponent<DialogueTrigger>().TriggerDialogue ();
		}
		*/
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") == true)
        {
            interactIcon.GetComponent<SpriteRenderer>().enabled = true;
            other.gameObject.GetComponent<PlayerController2D>().SetInteractable(this);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") == true)
        {
            interactIcon.GetComponent<SpriteRenderer>().enabled = false;
            other.gameObject.GetComponent<PlayerController2D>().SetInteractable(null);
        }
    }
}
