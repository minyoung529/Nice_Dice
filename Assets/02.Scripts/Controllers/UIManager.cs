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
        EventManager<List<object>>.StartListening(Define.ON_END_ROLL, CreateDamageEquation);
    }

    private void CreateDamageEquation(List<object> sides)
    {
        IReadOnlyList<Dice> selected = GameManager.Instance.SelectedDices;
        List<int> numbers = new List<int>();
        int multiply = -1;

        for (int i = 0; i < sides.Count; i++)
        {
            try
            {
                int num = (int)sides[i];

                if (selected[i].DiceType == DiceType.Number)
                    numbers.Add(num);
                else
                    multiply = num;
            }
            catch { }
        }

        // 일반 숫자가 1이 아니고, 곱하기가 하나라도 없을 경우
        // -> 괄호를 쓰지 않는다.
        if (numbers.Count != 1 && multiply != -1)
        {
            Debug.Log(string.Join("+", numbers));
        }
        else
        {
            Debug.Log("(" + string.Join("+", numbers) + ") X " + multiply);
        }
    }

    public override void OnStart()
    {
        Debug.Log("UI MANAGER START");
    }
}
