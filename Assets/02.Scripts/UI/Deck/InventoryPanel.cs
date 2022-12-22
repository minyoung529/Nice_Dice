using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryPanel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Dice dice;
    public Dice Dice => dice;
    private DeckUIController deckCtrl;
    private int index = 0;

    [SerializeField] Text nameText;

    private FollowTarget DiceObject => deckCtrl.diceObjects[index];

    public void Init(int index, Dice dice)
    {
        this.index = index;
        SetDice(dice);
        deckCtrl = FindObjectOfType<DeckUIController>();
    }

    public void SetDice(Dice dice)
    {
        this.dice = dice;
        if (dice)
        {
            UISetting();
        }
    }

    private void UISetting()
    {
        nameText.text = dice.DiceName;
    }

    private void DeselectItem()
    {
        //DeckUIController.CurrentEquipPanel.OnDeselctItem -= DeselectItem;
        DiceObject.ChangeTarget(transform);
        gameObject.SetActive(true);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DiceObject.ChangeTarget(null);
    }

    public void OnDrag(PointerEventData eventData) { }

    public void OnEndDrag(PointerEventData eventData)
    {
        EquipPanel equipPanel = DeckUIController.CurrentEquipPanel;

        // 장착할 수 있으면
        if (equipPanel != null && equipPanel.EquipDice(dice, index, true, this))
        {
            Equip(equipPanel);
        }
        else
        {
            DiceObject.ChangeTarget(transform);   // 원래 자리로 돌아오기
        }
    }

    public void Equip(EquipPanel equipPanel)
    {
        equipPanel.OnDeselctItem = null;
        equipPanel.OnDeselctItem += DeselectItem;

        DiceObject.ChangeTarget(equipPanel.transform);    // 해당 panel로 가기
        //equipPanel.EquipDice(dice, index, true);          // 장착!

        gameObject.SetActive(false);
    }
}