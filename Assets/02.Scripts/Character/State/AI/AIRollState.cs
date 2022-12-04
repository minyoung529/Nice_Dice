using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRollState : RollState
{
    public AIRollState(Character character) : base(character)
    {
    }

    protected override void ChildStart()
    {
        Debug.Log("AI 주사위 나감");
    }
}
