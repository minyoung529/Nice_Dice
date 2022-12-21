using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    [SerializeField]
    private AudioClip bgmClip;

    void Start()
    {
        EventManager.TriggerEvent(Define.ON_RELOAD_GAME);

        GameManager.Instance.UI.HeaderUI.UpdateUI();
        GameManager.Instance.UI.DescriptionUI.ActivePanel();
    }

    public void StartGame()
    {
        GameManager.Instance.NextTurn();
        SoundManager.Instance.Play(AudioType.BGM, bgmClip);
    }
}
