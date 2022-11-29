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
        DiceGague diceGague = FindObjectOfType<DiceGague>();
        Button button = GetComponent<Button>();

        button.onClick.AddListener(() => diceGague.IsPlaying = false);
        for (int i = 0; i < 3; i++)
        {
            button.onClick.AddListener(() => diceManager.SelectedDice[i].GetComponent<DiceControl>().IsRotate = true);
        }
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
