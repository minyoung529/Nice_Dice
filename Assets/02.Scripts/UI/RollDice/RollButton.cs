using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollButton : MonoBehaviour
{
    private void Start()
    {
        DiceGague diceGague = FindObjectOfType<DiceGague>();
        Button button = GetComponent<Button>();

        button.onClick.AddListener(() => diceGague.IsPlaying = false);
    }
}
