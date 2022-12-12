using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class EquipPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public DiceType equipDiceType;
    private Dice dice;

    private int index = -1;
    private DeckUIController deckCtrl;
    private Image image;

    #region Property
    private FollowTarget DiceObject => deckCtrl.diceObjects[index];
    public bool IsEmpty => dice == null;
    public Dice Dice => dice;
    public Action OnDeselctItem { get; set; }
    #endregion // Property

    private void Awake()
    {
        deckCtrl = FindObjectOfType<DeckUIController>();
        image = GetComponent<Image>();
    }

    #region Select
    public void OnPointerEnter(PointerEventData eventData)
    {
        image.DOKill();
        image.DOColor(Color.gray, 0.5f);
        DeckUIController.CurrentEquipPanel = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.DOKill();
        image.DOColor(Color.white, 0.5f);
        DeckUIController.CurrentEquipPanel = null;
    }
    #endregion

    #region Equip
    public bool EquipDice(Dice dice, int index, bool isAdd = false)
    {
        if (dice == null || dice.DiceType != equipDiceType || this.dice != null) return false;

        // type이 같고 max 개수를 넘지 않았을 때
        if (dice.DiceType == DiceType.Number && DiceCount(DiceType.Number) > Define.MAX_NUMBER_DICE) return false;
        if (dice.DiceType == DiceType.Skill && DiceCount(DiceType.Skill) > Define.MAX_SKILL_DICE) return false;
        if (dice.DiceType == DiceType.Multiply && DiceCount(DiceType.Multiply) > Define.MAX_MULTIPLY_DICE) return false;

        this.dice = dice;
        this.index = index;

        if (isAdd)
            GameManager.Instance.Deck.Add(dice);

        return true;
    }

    int DiceCount(DiceType type)
    {
        return GameManager.Instance.Deck.FindAll(x => x.DiceType == type).Count;
    }

    private void DelsetcItem()
    {
        OnDeselctItem.Invoke();
    }
    #endregion // Equip

    public void SwapDice(Dice dice, int index, Action deselect)
    {
        this.dice = dice;
        this.index = index;
        OnDeselctItem = deselect;

        if (index != -1)
        {
            deckCtrl.diceObjects[index]?.ChangeTarget(transform);
        }
    }

    #region Drag
    public void OnBeginDrag(PointerEventData eventData)
    {
        DiceObject.ChangeTarget(null);
        deckCtrl.InventoryInput.Active();
    }

    public void OnDrag(PointerEventData eventData) { }

    public void OnEndDrag(PointerEventData eventData)
    {
        EquipPanel curPanel = DeckUIController.CurrentEquipPanel;

        // Swap
        if (curPanel != null)
        {
            Dice tmpDice = dice;
            int tmpIdx = index;
            Action tempAct = OnDeselctItem;

            SwapDice(curPanel.dice, curPanel.index, curPanel.OnDeselctItem);
            curPanel.SwapDice(tmpDice, tmpIdx, tempAct);
        }
        // Inventory
        else if (InventoryInput.IsInventoryInput)
        {
            DelsetcItem();
            ResetValue();
        }
        // Back
        else
        {
            DiceObject.ChangeTarget(transform);
        }

        deckCtrl.InventoryInput.Inactive();
    }
    #endregion Drag

    private void ResetValue()
    {
        GameManager.Instance.Deck.Remove(dice);
        dice = null;
        index = -1;
    }
}
