using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIRollState : RollState
{
    Monster monsterData;

    public AIRollState(Character character) : base(character)
    {
        monsterData = character.GetComponent<AIEnemyController>().monsterData;
    }

    protected override void ChildStart()
    {
        DiceManager diceManager = GameManager.Instance.Dice;

        List<Dice> selected;
        List<Dice> monsterDice = new List<Dice>(monsterData.MonsterDices);

        if (character.HaveBlock)
        {
            Dice block = monsterDice.Find(x => x.DiceName.Contains("∫¿º‚"));
            monsterDice.Remove(block);
            character.HaveBlock = false;
        }

        selected = GameManager.Instance.Dice.DiceSelect(monsterDice);

        if(selected.Find(x=>x.DiceName.Contains("∫¿º‚")))
        {
            character.HaveBlock = true;
        }

        List<KeyValuePair<Dice, int>> selectedSides = diceManager.RollRandomDice(Random.Range(0, 12), character.transform.position, false, selected);

        // ¡÷ªÁ¿ßøÕ ∏È ¿Ã∫•∆Æ∑Œ ∫∏≥ª±‚
        EventManager<List<KeyValuePair<Dice, int>>>.TriggerEvent(Define.ON_END_ROLL, selectedSides);
    }
}
