using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorCollider : MonoBehaviour {

	public Transform player;
	public Transform elevator;

	public void OnTriggerEnter2D(Collider2D other) {
		player.parent = elevator;
	}

	public void OnTriggerExit2D(Collider2D other) {
		player.parent = null;
	}
}
