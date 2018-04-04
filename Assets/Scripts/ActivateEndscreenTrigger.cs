using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEndscreenTrigger : MonoBehaviour
{

    public GameObject endScreen;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("WHAY");
            endScreen.SetActive(true);
        }
    }
}
