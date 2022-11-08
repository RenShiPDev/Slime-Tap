using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishText : MonoBehaviour
{
    [SerializeField] private Text _finishText;
    public void InitText(string text)
    {
        _finishText.text = text;
    }
}
