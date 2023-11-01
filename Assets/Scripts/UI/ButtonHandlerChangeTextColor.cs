using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHandlerChangeTextColor : ButtonHandler
{
    private TextMeshProUGUI text;

    public Color selectedColor;
    private Color deselectedColor;

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);

        text.color = deselectedColor;
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);

        text.color = selectedColor;
    }

    public override void Start()
    {
        base.Start();

        text = GetComponentInChildren<TextMeshProUGUI>();
        deselectedColor = text.color;
    }
}
