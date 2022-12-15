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
    [field: SerializeField]
    private Character enemy;
    public Character Enemy
    {
        get
        {
            if (IsPlayer)
                enemy ??= FindObjectOfType<AIEnemyController>();
            else
                enemy ??= GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();

            return enemy;
        }
        set => enemy = value;
    }

    [SerializeField] private int hp;
    public int Hp
    {
        get => hp;
        set => hp = value;
    }

    [SerializeField] private int maxHp;
    public int MaxHp { get => maxHp; }

    [field: SerializeField]
    public bool IsPlayer { get; set; }

    Quaternion rot;

    public bool IsBlock { get; set; }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        rot = transform.rotation;
    }

    protected virtual void Start()
    {
        stateActions.Add(CharacterState.Idle, new IdleState(this));
        stateActions.Add(CharacterState.Attack, new AttackState(this));
        stateActions.Add(CharacterState.Hit, new HitState(this));
        stateActions.Add(CharacterState.BeforeRoll, new BeforeRollState(this));
        stateActions.Add(CharacterState.Roll, new RollState(this));
        stateActions.Add(CharacterState.Die, new DieState(this));

        foreach (var pair in stateActions)
        {
            pair.Value.OnAwake();
        }

        ChangeState(CharacterState.Idle);
    }

    public void ChangeState(CharacterState state)
    {
        currentState?.OnEnd();

        this.state = state;

        stateActions[state]?.ReceiveData(currentState?.SendedData);
        stateActions[state]?.OnStart();

        currentState = stateActions[state];
    }

    private void Update()
    {
        currentState?.OnUpdate();
        transform.rotation = rot;
    }

    private void OnDestroy()
    {
        foreach (var pair in stateActions)
        {
            pair.Value.OnDestroy();
        }
    }
}
