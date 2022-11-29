using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ֻ����� ������ �� �غ� �ڼ� ����
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
