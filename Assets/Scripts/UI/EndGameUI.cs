using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameUI : MonoBehaviour {

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("space") || Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene("MainScene");
        }
        else if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }

    }
}
