using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC: MonoBehaviour {

	public GameObject player;
	public GameObject speechReady;

	private int speechDistance = 2;
	private bool speaking;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		// Check if the player is in speech distance
		if ((player.transform.position - this.transform.position).sqrMagnitude < speechDistance) {
			speechReady.GetComponent<SpriteRenderer>().enabled = true;
			// Start conversation
			if (Input.GetKeyDown("e") && !speaking) {
				speaking = true;
				GetComponent<DialogueTrigger>().TriggerDialogue ();
			}
		} else {
			speechReady.GetComponent<SpriteRenderer> ().enabled = false;
		}
	}
}
