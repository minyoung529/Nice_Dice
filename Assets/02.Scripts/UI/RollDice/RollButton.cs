using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

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
            //diceManager.DiceCreate();
            diceGague.IsPlaying = false;

            // 주사위와 면 이벤트로 보내기
            EventManager<List<KeyValuePair<Dice, int>>>.TriggerEvent
                (Define.ON_END_ROLL, diceManager.RollRandomDice(diceGague.RollGrade, character.transform.position, true));

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