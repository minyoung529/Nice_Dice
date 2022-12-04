using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected CharacterState state = CharacterState.Idle;
    public CharacterState CurState => state;

    protected Dictionary<CharacterState, StateBase> stateActions = new Dictionary<CharacterState, StateBase>();
    protected StateBase currentState;

    public Animator Animator { get; protected set; }
    [field:SerializeField]
    public Character Enemy { get; set; }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        stateActions.Add(CharacterState.Idle, new IdleState(this));
        stateActions.Add(CharacterState.Attack, new AttackState(this));
        stateActions.Add(CharacterState.Hit, new HitState(this));
        stateActions.Add(CharacterState.BeforeRoll, new BeforeRollState(this));
        stateActions.Add(CharacterState.Roll, new RollState(this));

        foreach (var pair in stateActions)
        {
            pair.Value.OnAwake();
        }

        ChangeState(CharacterState.Idle);
    }

    public void ChangeState(CharacterState state)
    {
        if (state == CharacterState.Idle)
            Debug.Log("IDLE");
        currentState?.OnEnd();
        
        this.state = state;

        stateActions[state]?.ReceiveData(currentState?.SendedData);
        stateActions[state]?.OnStart();

        currentState = stateActions[state];
    }

    private void Update()
    {
        currentState?.OnUpdate();
    }
}
