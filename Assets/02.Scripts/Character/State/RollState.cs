using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

/// <summary>
/// �ֻ����� ���� ���� ����
/// </summary>
public class RollState : StateBase
{
    int rollHash = Animator.StringToHash("Roll");
    private readonly float delayTime = 2f;
    private float timer;
    Quaternion rot;

    public RollState(Character character) : base(character) { }

    public override void OnStart()
    {
        character.Animator.SetTrigger(rollHash);
        timer = 0f;
        rot = character.transform.rotation;
        ChildStart();
    }

    protected virtual void ChildStart() { }

    public override void OnUpdate()
    {
        timer += Time.deltaTime;
        character.transform.rotation = rot;
        if (timer > delayTime)
        {
            character.ChangeState(CharacterState.Attack);
        }
    }
}