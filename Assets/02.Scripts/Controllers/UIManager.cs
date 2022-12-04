using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomLib;

/// <summary>
/// UI�� �����ϴ� ��Ʈ�ѷ� (�ణ ������ ���)
/// </summary>
public class UIManager : ControllerBase
{
    private DamageText damageText;

    public override void OnAwake()
    {
        EventManager<List<KeyValuePair<Dice, int>>>.StartListening(Define.ON_END_ROLL, CreateDamageEquation);

        damageText = Object.FindObjectOfType<DamageText>();
    }

    private void CreateDamageEquation(List<KeyValuePair<Dice, int>> sides)
    {
        List<int> numbers = new List<int>();
        string result = "";
        int multiply = -1;

        for (int i = 0; i < sides.Count; i++)
        {
            Dice dice = sides[i].Key;
            try
            {
                if (dice.DiceType == DiceType.Number)
                {
                    numbers.Add(dice.numbers[sides[i].Value]);
                }
                else if (dice.DiceType == DiceType.Multiply)
                {
                    multiply = dice.numbers[sides[i].Value];
                }
            }
            catch { }
        }

        // �Ϲ� ���ڰ� 1�� �ƴϰ�, ���ϱⰡ �ϳ��� ���� ���
        // -> ��ȣ�� ���� �ʴ´�.
        if (multiply == -1)
        {
            result = string.Join("+", numbers);
        }
        else
        {
            if (numbers.Count == 1)
            {
                result = numbers[0] + " x " + multiply;
            }
            else
            {
                result = "(" + string.Join("+", numbers) + ") x " + multiply;
            }
        }

        damageText.Text(result);
    }

    public override void OnStart()
    {
        Debug.Log("UI MANAGER START");
    }
}
