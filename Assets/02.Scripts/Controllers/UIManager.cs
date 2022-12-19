using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomLib;
using System.Text;
using TMPro;

/// <summary>
/// UI를 관리하는 컨트롤러 (약간 윤지쌤 방식)
/// </summary>
public class UIManager : ControllerBase
{
    private DamageText equationText;
    private DamageText damageText;
    private DamageText effectText;
    private EnableShake evasionText;

    #region Property
    private DamageText EquationText
    { get { if (!equationText) equationText = GameObject.Find("EquationText").GetComponent<DamageText>(); return equationText; } }

    private DamageText DamageText
    { get { if (!damageText) damageText = GameObject.Find("DamageText").GetComponent<DamageText>(); return damageText; } }

    private DamageText EffectText
    { get { if (!effectText) effectText = GameObject.Find("EffectText").GetComponent<DamageText>(); return effectText; } }

    private EnableShake EvasionText
    { get { if (!evasionText) evasionText = Object.FindObjectOfType<EnableShake>(); return evasionText; } }
    #endregion

    private DescriptionUI descriptionUI = null;

    private HeaderUIController headerUIController = null;

    private WinUIController winUIController;

    #region Property
    public WinUIController WinUI
    {
        get
        {
            if (winUIController == null)
                winUIController = Object.FindObjectOfType<WinUIController>();

            return winUIController;
        }
    }
    public DescriptionUI DescriptionUI
    {
        get
        {
            if (descriptionUI == null)
            {
                descriptionUI = Object.FindObjectOfType<DescriptionUI>();
            }
            return descriptionUI;
        }
    }
    public HeaderUIController HeaderUI
    {
        get
        {
            if (headerUIController == null)
            {
                headerUIController = Object.FindObjectOfType<HeaderUIController>();
                if (headerUIController == null)
                {
                    headerUIController = GameObject.Find("Header UI")?.AddComponent<HeaderUIController>();
                }
            }
            return headerUIController;
        }
    }
    #endregion

    public override void OnAwake()
    {
        EventManager<List<KeyValuePair<Dice, int>>>.StartListening(Define.ON_END_ROLL, CreateDamageEquation);
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

        // 일반 숫자가 1이 아니고, 곱하기가 하나라도 없을 경우
        // -> 괄호를 쓰지 않는다.
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

        EquationText.Text(result.ToString());
        DamageText.Text(damage.ToString());

        if (GameManager.Instance.PlayerTurn)
            GameManager.maxDeal = Mathf.Max(damage, GameManager.maxDeal);

        EventManager<int>.TriggerEvent(Define.ON_END_ROLL, damage);
    }

    public void ActiveEffectText(string text)
    {
        EffectText.Text(text);
    }

    public void ActiveEffectText(MonsterType type)
    {
        //if (EvasionText) return;

        switch (type)
        {
            case MonsterType.Odd:
                EvasionText.Text("짝수 회피");
                break;
            case MonsterType.Even:
                EvasionText.Text("홀수 회피");
                break;
            case MonsterType.Range:
                EvasionText.Text("범위 회피");
                break;
        }

        EvasionText.Active();
    }

    ~UIManager()
    {
        EventManager<List<KeyValuePair<Dice, int>>>.StopListening(Define.ON_END_ROLL, CreateDamageEquation);
    }
}
