using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRollState : RollState
{
    public AIRollState(Character character) : base(character)
    {
    }

    protected override void ChildStart()
    {
        DiceManager diceManager = GameManager.Instance.Dice;

        diceManager.DiceThrow();

        List<KeyValuePair<Dice, int>> selectedSides = new List<KeyValuePair<Dice, int>>();

        for (int i = 0; i < 3; i++)
        {
            DiceShape shape = diceManager.SelectedDice[i].DiceShape;
            int side = diceManager.DiceSideSelect(shape, Random.Range(0, 12));
            diceManager.DiceObjects[i].GetComponent<DiceControl>().DiceSideUp(shape, side);

            selectedSides.Add(new KeyValuePair<Dice, int>(diceManager.SelectedDice[i], side));
        }

        // 주사위와 면 이벤트로 보내기
        EventManager<List<KeyValuePair<Dice, int>>>.TriggerEvent(Define.ON_END_ROLL, selectedSides);
    }
}
