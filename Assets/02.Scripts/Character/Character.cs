using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Character : MonoBehaviour
{
    protected Animator animator;
    protected CharacterState state;
    protected Dictionary<CharacterState, Action> stateActions = new Dictionary<CharacterState, Action>();

    private void Start()
    {
        animator = GetComponent<Animator>();

        for (int i = 0; i < (int)CharacterState.Length; i++)
        {
            stateActions.Add((CharacterState)i, () => { });
        }
    }

    public void ChangeState(CharacterState state)
    {
        this.state = state;
        animator.Play(state.ToString());
        stateActions[state]?.Invoke();
    }

    public void RegisterAction(CharacterState state, Action action)
    {
        stateActions[state] += action;
    }
}
