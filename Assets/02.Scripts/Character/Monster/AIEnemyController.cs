using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemyController : Character
{
    protected override void Start()
    {
        base.Start();

        EventManager.StartListening(Define.ON_START_MONSTER_TURN, Roll);

        stateActions[CharacterState.Roll] = new AIRollState(this);
        stateActions[CharacterState.Attack] = new AIAttackState(this);
    }

    private void Roll()
    {
        Debug.Log("ROLL");
        ChangeState(CharacterState.Roll);
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Define.ON_START_MONSTER_TURN, Roll);
    }
}
