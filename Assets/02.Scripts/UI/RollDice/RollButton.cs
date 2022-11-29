using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollButton : MonoBehaviour
{
    private void Start()
    {
        DiceGague diceGague = FindObjectOfType<DiceGague>();
        DiceManager diceManager = FindObjectOfType<DiceManager>();
        Button button = GetComponent<Button>();

        button.onClick.AddListener(() => diceGague.IsPlaying = false);
        for (int i = 0; i < 3; i++)
        {
            button.onClick.AddListener(() => diceManager.SelectedDice[i].GetComponent<DiceControl>().IsRotate = true);
        }
    }
}