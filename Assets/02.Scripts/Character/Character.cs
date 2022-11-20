using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected MeshRenderer meshRenderer;
    [SerializeField]
    protected Material[] materials = new Material[(int)CharacterState.Length];
    protected CharacterState state;
    protected Dictionary<CharacterState, StateBase> stateActions = new Dictionary<CharacterState, StateBase>();
    protected StateBase currentState;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        stateActions.Add(CharacterState.Idle, new IdleState(this));
        stateActions.Add(CharacterState.Attack, new AttackState(this));
        stateActions.Add(CharacterState.Hit, new HitState(this));

        foreach (var pair in stateActions)
        {
            pair.Value.OnAwake();
        }

        ChangeState(CharacterState.Idle);
    }

    public void ChangeState(CharacterState state)
    {
        this.state = state;

        currentState?.OnEnd();
        meshRenderer.material = materials[(int)state];

        stateActions[state]?.ReceiveData(currentState?.SendedData);
        stateActions[state]?.OnStart();

        currentState = stateActions[state];
    }

    private void Update()
    {
        currentState?.OnUpdate();
    }
}
