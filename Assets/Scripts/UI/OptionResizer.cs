using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This class resizes the button for the option to match the amount of text displayed
/// </summary>
public class OptionResizer : MonoBehaviour
{

    private TextMeshProUGUI text;
    private LayoutElement layoutElement;

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        layoutElement = GetComponent<LayoutElement>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        string optionText = text.text;
        layoutElement.preferredHeight = (1 + (int)(optionText.Length / 50)) * 20;
    }
}
