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
        Debug.Log("AI �ֻ��� ����");
    }
}
