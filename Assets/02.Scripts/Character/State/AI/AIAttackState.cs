using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIAttackState : AttackState
{

    public AIAttackState(Character character) : base(character)
    {
    }

    protected override void ChildStart()
    {
    }

}
