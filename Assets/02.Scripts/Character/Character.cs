using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Character : MonoBehaviour
{
    protected Animator animator;
    protected CharacterState state;
    protected Dictionary<CharacterState, StateBase> stateActions = new Dictionary<CharacterState, StateBase>();
    protected StateBase currentState;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        stateActions.Add(CharacterState.Idle, new IdleState(this));
        stateActions.Add(CharacterState.Attack, new AttackState(this));

        ChangeState(CharacterState.Idle);

        foreach (var pair in stateActions)
        {
            pair.Value.OnAwake();
        }
    }

    public void ChangeState(CharacterState state)
    {
        this.state = state;
        animator.Play(state.ToString());

        currentState?.OnEnd();

        stateActions[state]?.ReceiveData(currentState?.SendedData);
        stateActions[state]?.OnStart();

        currentState = stateActions[state];
    }

    private void Update()
    {
        currentState?.OnUpdate();
    }
}
