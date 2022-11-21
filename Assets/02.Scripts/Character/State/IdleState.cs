using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IdleState : StateBase
{
    private Sequence seq;
    private Vector3 upPos;
    private Vector3 downPos;
    private const float FLOATING_DISTANCE = 0.5f;
    private const float FLOATING_TIME = 0.6f;

    public IdleState(Character character) : base(character) { }

    public override void OnAwake()
    {
        downPos = character.transform.position;
        upPos = downPos + Vector3.up * FLOATING_DISTANCE;
    }

    public override void OnStart()
    {
        sendedData = character.transform.position;

        seq = DOTween.Sequence();
        seq.Append(character.transform.DOMove(upPos, FLOATING_TIME));
        seq.Append(character.transform.DOMove(downPos, FLOATING_TIME));
        seq.SetLoops(-1);
    }

    public override void OnEnd()
    {
        seq.Kill();
    }
}
