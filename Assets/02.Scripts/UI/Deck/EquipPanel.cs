using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private DiceType diceType;
    private Dice dice;

    public Action OnDeselctItem { get; set; }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (dice == null)
            DeckUIController.CurrentEquipPanel = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DeckUIController.CurrentEquipPanel = null;
    }

    public bool EquipDice(Dice dice)
    {
        if (dice == null || dice.DiceType != diceType) return false;
        this.dice = dice;

        if (dice.DiceType == DiceType.Number && DiceCount(DiceType.Number) > Define.MAX_NUMBER_DICE) return false;
        if (dice.DiceType == DiceType.Skill && DiceCount(DiceType.Skill) > Define.MAX_SKILL_DICE) return false;
        if (dice.DiceType == DiceType.Multiply && DiceCount(DiceType.Multiply) > Define.MAX_MULTIPLY_DICE) return false;

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
}
