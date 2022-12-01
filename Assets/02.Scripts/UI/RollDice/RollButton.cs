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
    private DiceManager diceManager = null;

    private void Awake()
    {
        diceGague = FindObjectOfType<DiceGauge>();
        button = GetComponent<Button>();
        diceManager = FindObjectOfType<DiceManager>();

        button.onClick.AddListener(() => diceGague.IsPlaying = false);

        for (int i = 0; i < 3; i++)
        {
            button.onClick.AddListener(() =>
            {
                GameObject dice = Instantiate(diceManager.SelectedDice[i].DicePrefab);
                dice.GetComponent<DiceControl>().DiceThrow();
            });
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        diceManager.DiceSelect();
        diceGague.IsPlaying = true;
        character.ChangeState(CharacterState.BeforeRoll);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        diceGague.IsPlaying = false;
        character.ChangeState(CharacterState.Roll);
    }
}
