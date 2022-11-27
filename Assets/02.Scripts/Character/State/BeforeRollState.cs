using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 주사위를 굴리기 전 준비 자세 상태
/// </summary>
public class BeforeRollState : StateBase
{
    private int beforeRollHash = Animator.StringToHash("BeforeRoll");

    public BeforeRollState(Character character) : base(character) { }

    public override void OnStart()
    {
        character.Animator.SetBool(beforeRollHash, true);
    }

    public override void OnEnd()
    {
        character.Animator.SetBool(beforeRollHash, false);
    }
}
