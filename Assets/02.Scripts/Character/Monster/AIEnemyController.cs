using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemyController : Character
{
    public Monster monsterData = null;

    protected override void Start()
    {
        base.Start();

        EventManager.StartListening(Define.ON_START_MONSTER_TURN, MonsterTurn);

        stateActions[CharacterState.BeforeRoll] = new AIBeforeRollState(this);
        stateActions[CharacterState.Roll] = new AIRollState(this);
        stateActions[CharacterState.Attack] = new AIAttackState(this);
    }

    private void MonsterTurn()
    {
        ChangeState(CharacterState.BeforeRoll);
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Define.ON_START_MONSTER_TURN, MonsterTurn);
    }
}
