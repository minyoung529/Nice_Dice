using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RollButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public Character character;
    private DiceGauge diceGague;
    private Button button;

    private void Awake()
    {
        diceGague = FindObjectOfType<DiceGauge>();
        button = GetComponent<Button>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        diceGague.IsPlaying = true;
        character.ChangeState(CharacterState.BeforeRoll);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        diceGague.IsPlaying = false;
        character.ChangeState(CharacterState.Roll);
    }
}
