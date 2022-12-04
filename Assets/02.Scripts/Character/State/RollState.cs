using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 주사위를 굴릴 때의 상태
/// </summary>
public class RollState : StateBase
{
    int rollHash = Animator.StringToHash("Roll");

    public RollState(Character character) : base(character) { }

    public override void OnStart()
    {
        character.Animator.SetTrigger(rollHash);
        ChildStart();
    }

    protected virtual void ChildStart() { }

    public override void OnUpdate()
    {
        if (character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
        {
            character.ChangeState(CharacterState.Attack);
        }
    }
}