using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RollUI : MonoBehaviour
{
    private Vector2 originalPos;
    private Vector2 targetPos;

    private void Awake()
    {
        EventManager.StartListening(Define.ON_START_MONSTER_TURN, Inactive);
        EventManager.StartListening(Define.ON_START_PLAYER_TURN, Inactive);
        EventManager.StartListening(Define.ON_END_DRAW, Active);

        originalPos = transform.position;
        targetPos = originalPos + Vector2.down * 700f;
    }

    private void Active()
    {
        transform.DOMove(originalPos, 1f);
    }

    private void Inactive()
    {
        transform.DOMove(targetPos, 1f);
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Define.ON_START_MONSTER_TURN, Inactive);
        EventManager.StopListening(Define.ON_START_PLAYER_TURN, Inactive);
        EventManager.StopListening(Define.ON_END_DRAW, Active);
    }
}
