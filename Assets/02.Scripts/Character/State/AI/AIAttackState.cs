using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackState : AttackState
{
    public AIAttackState(Character character) : base(character)
    {
    }

    protected override void ChildStart()
    {
        Debug.Log("AI ATTACK!");
    }
}
