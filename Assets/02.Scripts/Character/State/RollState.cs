using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 주사위를 굴릴 때의 상태
/// </summary>
public class RollState : StateBase
{
    int rollHash = Animator.StringToHash("Roll");
    private readonly float delayTime = 2f;
    private float timer;

    public RollState(Character character) : base(character) { }

    public override void OnStart()
    {
        character.Animator.SetTrigger(rollHash);
        timer = 0f;
        Debug.Log("ROLL");
        ChildStart();
    }

    protected virtual void ChildStart() { }

    public override void OnUpdate()
    {
        timer += Time.deltaTime;

        if (timer > delayTime)
        {
            Debug.Log("WAITATTACK");
            character.ChangeState(CharacterState.Attack);
        }
    }
}