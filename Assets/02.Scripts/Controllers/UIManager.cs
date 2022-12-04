using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomLib;

/// <summary>
/// UI를 관리하는 컨트롤러 (약간 윤지쌤 방식)
/// </summary>
public class UIManager : ControllerBase
{
    public override void OnAwake()
    {
        EventManager<List<KeyValuePair<Dice, int>>>.StartListening(Define.ON_END_ROLL, CreateDamageEquation);
    }

    private void CreateDamageEquation(List<KeyValuePair<Dice, int>> sides)
    {
        List<int> numbers = new List<int>();
        int multiply = -1;

        for (int i = 0; i < sides.Count; i++)
        {
            Dice dice = sides[i].Key;
            try
            {
                Debug.Log(dice.DiceType + ", " + dice.numbers[sides[i].Value]);

                if (dice.DiceType == DiceType.Number)
                    numbers.Add(dice.numbers[sides[i].Value]);
                else if (dice.DiceType == DiceType.Multiply)
                    multiply = dice.numbers[sides[i].Value];
            }
            catch { }
        }

        // 일반 숫자가 1이 아니고, 곱하기가 하나라도 없을 경우
        // -> 괄호를 쓰지 않는다.
        if (multiply == -1)
        {
            Debug.Log(string.Join("+", numbers));
        }
        else
        {
            if (numbers.Count == 1)
            {
                Debug.Log(numbers[0] + " x " + multiply);
            }
            else
            {
                Debug.Log("(" + string.Join("+", numbers) + ") x " + multiply);
            }
        }
    }

    public override void OnStart()
    {
        Debug.Log("UI MANAGER START");
    }
}
