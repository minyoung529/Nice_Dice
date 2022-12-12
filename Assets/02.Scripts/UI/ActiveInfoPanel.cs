using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActiveInfoPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    EquipPanel equipPanel;
    InventoryPanel inventoryPanel;
    DeckUIController controller;
    Dice dice;

    private void Start()
    {
        equipPanel = GetComponent<EquipPanel>();
        inventoryPanel = GetComponent<InventoryPanel>();
        controller = FindObjectOfType<DeckUIController>();
        if (equipPanel)
        {
            dice = equipPanel.Dice;
        }
        else
        {
            dice = inventoryPanel.Dice;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        controller.InfoPanel.Active(dice, transform.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        controller.InfoPanel.Inactive();
    }
}
