using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HitState : StateBase
{
    private Sequence seq;
    private const float BACK_DISTANCE = 0.5f;
    private const float BACK_TIME = 0.2f;

    public HitState(Character character) : base(character) { }

    public override void OnStart()
    {
        seq = DOTween.Sequence();

        Vector3 originalPos = character.transform.position;
        Vector3 hitPos = originalPos - Vector3.right * BACK_DISTANCE;

        seq.Append(character.transform.DOMove(hitPos, BACK_TIME));
        seq.AppendInterval(0.2f);
        seq.Append(character.transform.DOMove((Vector3)receiveData, 0.1f));
        seq.AppendCallback(() => character.ChangeState(CharacterState.Idle));
    }

    public override void OnEnd()
    {
        seq.Kill();
    }
}