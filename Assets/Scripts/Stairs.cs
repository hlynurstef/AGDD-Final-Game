using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") == true)
        {
            other.gameObject.GetComponent<PlayerController>().SetStairs(this);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") == true)
        {
            other.gameObject.GetComponent<PlayerController>().SetStairs(null);
        }
    }
}
