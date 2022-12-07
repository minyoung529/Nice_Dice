using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 공격 상태
/// </summary>
public class AttackState : StateBase
{
    private readonly int attackHash = Animator.StringToHash("Attack");
    private const float ATTACK_TIME = 2f;
    private float timer = ATTACK_TIME;

    public AttackState(Character character) : base(character) { }

    public override void OnStart()
    {
        character.Animator.SetTrigger(attackHash);
    }

    protected virtual void ChildStart() { }

    public override void OnUpdate()
    {
        base.OnUpdate();

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            character.Enemy.ChangeState(CharacterState.Hit);
            character.ChangeState(CharacterState.Idle);
        }
    }

    public override void OnEnd()
    {
        timer = ATTACK_TIME;
    }
}
