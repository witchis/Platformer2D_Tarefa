using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SOUIIntUpdate : MonoBehaviour
{
    public SOInt soInt;
    public TextMeshProUGUI uiTextValue;

    private void Start()
    {
        uiTextValue.text = $"x {soInt.value}";
    }

    private void Update()
    {
        uiTextValue.text = $"x {soInt.value}";
    }
}
