using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RollButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public Character character;
    private DiceGauge diceGague;
    private DiceManager diceManager = null;
    private bool canRoll = false;

    private void Awake()
    {
        diceGague = FindObjectOfType<DiceGauge>();
        diceManager = FindObjectOfType<DiceManager>();
        EventManager.StartListening(Define.ON_START_PLAYER_TURN, OnTurn);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (canRoll)
        {
            diceGague.IsPlaying = true;
            character.ChangeState(CharacterState.BeforeRoll);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (canRoll)
        {
            diceManager.DiceCreate();
            diceGague.IsPlaying = false;

            List<KeyValuePair<Dice, int>> selectedSides = new List<KeyValuePair<Dice, int>>();

            for (int i = 0; i < 3; ++i)
            {
                DiceShape shape = diceManager.SelectedDice[i].DiceShape;
                int side = diceManager.DiceSideSelect(shape, diceGague.RollGrade);

                DiceControl control = diceManager.DiceObjects[i].GetComponent<DiceControl>();
                control.SetValue(shape, side);
                control.DiceThrow();

                selectedSides.Add(new KeyValuePair<Dice, int>(diceManager.SelectedDice[i], side));
            }

            // 주사위와 면 이벤트로 보내기
            EventManager<List<KeyValuePair<Dice, int>>>.TriggerEvent(Define.ON_END_ROLL, selectedSides);

            character.ChangeState(CharacterState.Roll);
            canRoll = false;
        }
    }

    private void OnTurn()
    {
        canRoll = true;
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Define.ON_START_PLAYER_TURN, OnTurn);
    }
}