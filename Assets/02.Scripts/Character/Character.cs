using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected Animator animator;
    protected CharacterState state;
    protected Dictionary<CharacterState, Action> stateActions;

    public void ChangeState(CharacterState state)
    {
        this.state = state;
        animator.Play(state.ToString());
        stateActions[state].Invoke();
    }

    public void RegisterAction(CharacterState state, Action action)
    {
        stateActions[state] += action;
    }
}
