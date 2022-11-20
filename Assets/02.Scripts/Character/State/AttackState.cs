using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AttackState : StateBase
{
    private Sequence seq;

    public AttackState(Character character) : base(character) { }

    public override void OnStart()
    {
        seq = DOTween.Sequence();

        seq.Append(character.transform.DOShakePosition(1f));
        seq.Append(character.transform.DOMove((Vector3)receiveData, 1f));
        //seq.AppendCallback(()=> character.ChangeState(CharacterState.Idle));
    }

    public override void OnEnd()
    {
        seq.Kill();
    }
}
