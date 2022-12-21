using System.Collections;
using System.Collections.Generic;
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

        List<KeyValuePair<Dice, int>> selectedSides 
            = diceManager.RollRandomDice(Random.Range(0,12), character.transform.position, false, monsterData.MonsterDices);

        // �ֻ����� �� �̺�Ʈ�� ������
        EventManager<List<KeyValuePair<Dice, int>>>.TriggerEvent(Define.ON_END_ROLL, selectedSides);
    }
}
