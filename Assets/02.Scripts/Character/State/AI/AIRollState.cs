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

        List<KeyValuePair<Dice, int>> selectedSides 
            = diceManager.RollRandomDice(Random.Range(0,12), character.transform.position, false);

        // �ֻ����� �� �̺�Ʈ�� ������
        EventManager<List<KeyValuePair<Dice, int>>>.TriggerEvent(Define.ON_END_ROLL, selectedSides);


    }
}
