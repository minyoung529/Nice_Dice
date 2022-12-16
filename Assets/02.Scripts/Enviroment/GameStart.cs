using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    void Start()
    {
        EventManager.TriggerEvent(Define.ON_RELOAD_GAME);

        GameManager.Instance.NextTurn();
        //GameManager.Instance.stage = 1;
        GameManager.Instance.UI.HeaderUIController.UpdateUI();
    }
}