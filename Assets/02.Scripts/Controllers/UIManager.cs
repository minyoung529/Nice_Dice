using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomLib;

/// <summary>
/// UI�� �����ϴ� ��Ʈ�ѷ� (�ణ ������ ���)
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

        // �Ϲ� ���ڰ� 1�� �ƴϰ�, ���ϱⰡ �ϳ��� ���� ���
        // -> ��ȣ�� ���� �ʴ´�.
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
