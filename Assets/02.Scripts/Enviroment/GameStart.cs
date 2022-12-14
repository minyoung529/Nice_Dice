using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.NextTurn();
        GameManager.Instance.stage = 1;
        GameManager.Instance.UI.HeaderUIController.UpdateUI();
    }
}