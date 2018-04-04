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

    // Use this for initialization
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        layoutElement = GetComponent<LayoutElement>();

        string optionText = text.text;
        layoutElement.preferredHeight = (1 + (int)(optionText.Length / 50)) * 20;
    }
}
