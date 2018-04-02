using UnityEngine;
using Yarn.Unity;

public class CanSpeak : MonoBehaviour
{
    /// <summary>
    /// The avatar image that should be displayed in the dialogue window.
    /// </summary>
	[SerializeField, Tooltip("The avatar image that should be displayed in the dialogue window.")]
    private Sprite _avatarImage;
    /// <summary>
    /// The name that should be displayed in the dialogue window.
    /// </summary>
	[SerializeField, Tooltip("The name that should be displayed in the dialogue window.")]
    private string _displayName;

    void OnEnable()
    {
        CheckValues();
    }

    void OnDisable()
    {
        CheckValues();
    }

    private void CheckValues()
    {
        if (_avatarImage == null)
        {
            Debug.LogError("Missing avatar image on game object: " + gameObject.name, gameObject);
        }

        if (string.IsNullOrEmpty(_displayName))
        {
            Debug.LogError("Missing display name on game object: " + gameObject.name, gameObject);
        }
    }


    /// <summary>
    /// Sets the avatar image and display name for this NPC to appear in the dialogue box.
    /// This function can be called with the set_avatar Yarn command.
    /// </summary>
    [YarnCommand("set_avatar")]
    public void SetAvatar()
    {
        print("setting avatar for " + gameObject.name);
        DialogueUI dialogueUI = FindObjectOfType<DialogueUI>();
        dialogueUI.SetAvatar(_avatarImage);
        dialogueUI.SetName(_displayName);
    }
}
