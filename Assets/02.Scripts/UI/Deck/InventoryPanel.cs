using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryPanel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Dice dice;

    public Action<int, GameObject> OnSetDice { get; private set; }
    public Action<Transform> OnChangeActive { get; set; }
    private int index = 0;

    [SerializeField] Text nameText;

    public void Init(int index, Dice dice, Action<int, GameObject> onSetDice)
    {
        this.index = index;
        OnSetDice += onSetDice;
        SetDice(dice);
    }

    public void SetDice(Dice dice)
    {
        this.dice = dice;
        UISetting();

        OnSetDice?.Invoke(index, dice.DicePrefab);
    }

    private void UISetting()
    {
        nameText.text = dice.DiceName;
    }

    private void DeselectItem()
    {
        DeckUIController.CurrentEquipPanel.OnDeselctItem -= DeselectItem;
        OnChangeActive.Invoke(transform);
        gameObject.SetActive(true);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (DeckUIController.CurrentEquipPanel != null && DeckUIController.CurrentEquipPanel.EquipDice(dice))
        {
            DeckUIController.CurrentEquipPanel.OnDeselctItem += DeselectItem;
            OnChangeActive.Invoke(DeckUIController.CurrentEquipPanel.transform);

            gameObject.SetActive(false);
        }
        else
        {
            OnChangeActive.Invoke(transform);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnChangeActive.Invoke(null);
    }
}
