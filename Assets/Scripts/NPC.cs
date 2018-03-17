using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable {

	public GameObject player;
	public GameObject speechReady;

	private int speechDistance = 2;
	private bool speaking;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
	}

	public void Interact()
	{
		// Check if the player is in speech distance
		if ((player.transform.position - this.transform.position).sqrMagnitude < speechDistance) {
			speechReady.GetComponent<SpriteRenderer>().enabled = true;
			// Start conversation
			if (speaking == false) {
				speaking = true;
				GetComponent<DialogueTrigger>().TriggerDialogue ();
			}
		} else {
			speechReady.GetComponent<SpriteRenderer> ().enabled = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player") == true)
		{
			other.gameObject.GetComponent<PlayerController2D>().SetInteractable(this);
		}	
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player") == true)
		{
			other.gameObject.GetComponent<PlayerController2D>().SetInteractable(null);
		}		
	}
}
