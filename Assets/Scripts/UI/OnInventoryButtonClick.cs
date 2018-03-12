using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnInventoryButtonClick : MonoBehaviour {

	public GameObject activated;
	public GameObject deactivated;

	void OnMouseDown() {
		activated.SetActive(false);
		deactivated.SetActive(true);
	}

	void OnMouseUp() {
		print("hello");
		activated.SetActive(true);
		deactivated.SetActive(false);
	}
}
