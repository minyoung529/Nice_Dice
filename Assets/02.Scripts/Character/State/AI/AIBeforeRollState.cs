using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBeforeRollState : BeforeRollState
{
    private const float delay = 2f;
    private float timer;

    public AIBeforeRollState(Character character) : base(character)
    {
    }

    public override void OnStart()
    {
        timer = delay;
    }

    public override void OnUpdate()
    {
        timer -= Time.deltaTime;

        if (timer < 0f)
        {
            character.ChangeState(CharacterState.Roll);
        }
    }
}
