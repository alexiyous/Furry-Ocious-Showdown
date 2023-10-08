using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Components : MonoBehaviour
{
    private Image spriteRenderer;
    [HideInInspector] public TokenSlotSO tokenSO;
    [HideInInspector] public ComponentManager componentManager;
    public ComponentType componentType;
    [HideInInspector] public bool shot = true;

    private void Awake()
    {
        spriteRenderer = GetComponent<Image>();
    }

    public void Set()
    {
        spriteRenderer.sprite = tokenSO.sprite;
        componentType = tokenSO.bulletType;
        shot = false;
    }

    public void SetCombo()
    {
        if (componentManager.comboCount >= 3) return;
        if (tokenSO != null)
        {
            componentManager.comboSlot[componentManager.comboCount].tokenSO = tokenSO;
            componentManager.comboSlot[componentManager.comboCount].Set();
            Unset(componentManager.defaultSprite);
            componentManager.comboCount++;
        }
    }

    public void Unset(Sprite emptySprite)
    {
        spriteRenderer.sprite = emptySprite;
        shot = true;
        tokenSO = null;
        componentType = ComponentType.None;
    }
}
