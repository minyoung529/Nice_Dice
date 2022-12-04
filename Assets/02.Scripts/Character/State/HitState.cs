using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 맞는 상태
/// </summary>
public class HitState : StateBase
{
    private const float BACK_DISTANCE = 0.5f;
    private const float BACK_TIME = 0.2f;

    private readonly int hitHash = Animator.StringToHash("Hit");
    private float timer;

    public HitState(Character character) : base(character) { }

    public override void OnStart()
    {
        character.Animator.SetTrigger(hitHash);
        timer = character.Animator.GetCurrentAnimatorClipInfo(0).Length;
    }

    public override void OnUpdate()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            character.ChangeState(CharacterState.Idle);
            GameManager.Instance.NextTurn();
        }
    }

    public override void OnEnd()
    {
    }
}