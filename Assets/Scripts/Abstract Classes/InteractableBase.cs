using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class InteractableBase : MonoBehaviour
{
    [Header("Interactable Settings")]
    [SerializeField, Tooltip("The GameObject to enable when player can interact with this container")]
    protected GameObject _interactIcon;
    [SerializeField, Tooltip("The Yarn node to play when interacting with this container")]
    private string _interactNode;

    void OnEnable()
    {
        CheckValues();
    }

    void OnDisable()
    {
        CheckValues();
    }

    /// <summary>
    /// Checks if all necessary values are set and logs an error if any necessary value is not set.
    /// </summary>
    private void CheckValues()
    {
        if (_interactIcon == null)
        {
            Debug.LogError("Missing interaction icon on game object: " + gameObject.name, gameObject);
        }
        else
        {
            _interactIcon.SetActive(false);
        }

        if (string.IsNullOrEmpty(_interactNode))
        {
            Debug.LogError("Missing interact node on game object: " + gameObject.name, gameObject);
        }
    }

    public void Interact()
    {
        FindObjectOfType<DialogueRunner>().StartDialogue(_interactNode);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") == true)
        {
            _interactIcon.SetActive(true);
            other.gameObject.GetComponent<PlayerController>().SetInteractable(this);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") == true)
        {
            _interactIcon.SetActive(false);
            other.gameObject.GetComponent<PlayerController>().SetInteractable(null);
        }
    }
}
