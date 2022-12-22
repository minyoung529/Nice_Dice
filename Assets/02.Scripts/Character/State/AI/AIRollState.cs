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

        List<Dice> selected = GameManager.Instance.Dice.DiceSelect(monsterData.MonsterDices);

        List<KeyValuePair<Dice, int>> selectedSides;
        bool isHaveBlock = false;

        int cnt = 0;

        do
        {
            selectedSides = diceManager.RollRandomDice(Random.Range(0, 12), character.transform.position, false, selected);

            if (selected.Find(x => x.DiceName.Contains("����")))
            {
                if (character.HaveBlock)
                {
                    isHaveBlock = true;
                    character.HaveBlock = false;
                }
                else
                {
                    character.HaveBlock = true;
                    isHaveBlock = false;
                }
            }
            else
                character.HaveBlock = false;

            if (cnt++ > 10)
                break;

        } while (isHaveBlock);


        // �ֻ����� �� �̺�Ʈ�� ������
        EventManager<List<KeyValuePair<Dice, int>>>.TriggerEvent(Define.ON_END_ROLL, selectedSides);
    }
}
