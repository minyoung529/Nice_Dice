using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    void Start()
    {
        EventManager.TriggerEvent(Define.ON_RELOAD_GAME);

        GameManager.Instance.UI.HeaderUIController.UpdateUI();
        GameManager.Instance.UI.DescriptionUI.ActivePanel();
    }

    public static void StartGame()
    {
        GameManager.Instance.NextTurn();
    }
}