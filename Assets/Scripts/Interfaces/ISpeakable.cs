using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public interface ISpeakable
{
    /// <summary>
    /// The avatar image that should be displayed in the dialogue window.
    /// </summary>
    Sprite AvatarImage { get; }
    /// <summary>
    /// The name that should be displayed in the dialogue window.
    /// </summary>
    string DisplayName { get; }


    /// <summary>
    /// Should set the avatar image and display name to appear in the dialogue box.
    /// </summary>
    void SetAvatar();
}
