using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomLib;
using System.Text;

/// <summary>
/// UI�� �����ϴ� ��Ʈ�ѷ� (�ణ ������ ���)
/// </summary>
public class UIManager : ControllerBase
{
    private DamageText equationText;
    private DamageText damageText;

    public override void OnAwake()
    {
        EventManager<List<KeyValuePair<Dice, int>>>.StartListening(Define.ON_END_ROLL, CreateDamageEquation);

        // ���� ��ġ��
        equationText = GameObject.Find("EquationText")?.GetComponent<DamageText>();
        damageText = GameObject.Find("DamageText")?.GetComponent<DamageText>();
    }

    private void CreateDamageEquation(List<KeyValuePair<Dice, int>> sides)
    {
        List<int> numbers = new List<int>();
        StringBuilder result = new StringBuilder();
        int multiply = -1;
        int damage = 0;

        for (int i = 0; i < sides.Count; i++)
        {
            Dice dice = sides[i].Key;
            try
            {
                if (dice.DiceType == DiceType.Number)
                {
                    numbers.Add(dice.numbers[sides[i].Value]);
                    damage += numbers[numbers.Count - 1];
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
            result.Append(string.Join('+', numbers));
        }
        else
        {
            if (numbers.Count == 1)
            {
                result.Append(numbers[0]);
                result.Append(" x ");
                result.Append(multiply);
            }
            else
            {
                result.Append('(');
                result.Append(string.Join('+', numbers));
                result.Append(") x ");
                result.Append(multiply);
            }

            damage *= multiply;
        }

        equationText ??= GameObject.Find("EquationText")?.GetComponent<DamageText>();
        damageText ??= GameObject.Find("DamageText")?.GetComponent<DamageText>();

        equationText.Text(result.ToString());
        damageText.Text(damage.ToString());

        EventManager<int>.TriggerEvent(Define.ON_END_ROLL, damage);
    }

    public override void OnStart()
    {
        Debug.Log("UI MANAGER START");
    }
}
