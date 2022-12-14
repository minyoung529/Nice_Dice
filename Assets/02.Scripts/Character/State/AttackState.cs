using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

/// <summary>
/// 공격 상태
/// </summary>
public class AttackState : StateBase
{
    private readonly int attackHash = Animator.StringToHash("Attack");
    private const float ATTACK_TIME = 0.63f;
    private float timer = ATTACK_TIME;
    private bool isEffect;
    private AttackEffect effect;
    GameObject curEffect;

    public AttackState(Character character) : base(character)
    {
        effect = character.GetComponent<AttackEffect>();
        EventManager<int>.StartListening(Define.ON_END_ROLL, SetAttackEffect);
    }

    public override void OnStart()
    {
        character.Animator.SetTrigger(attackHash);
        isEffect = false;

        if (SkillManager.CurrentSkill)
        {
            timer += 2f;
        }

        ChildStart();
    }

    protected virtual void ChildStart() { }

    public override void OnUpdate()
    {
        base.OnUpdate();

        timer -= Time.deltaTime;

        if (timer < ATTACK_TIME / 2f && !isEffect)
        {
            character.StartCoroutine(Effect());
        }

        if (timer < 0)
        {
            character.Enemy.ChangeState(CharacterState.Hit);
            character.ChangeState(CharacterState.Idle);
        }
    }

    public override void OnEnd()
    {
        timer = ATTACK_TIME;
    }

    private void SetAttackEffect(int damage)
    {
        if (effect == null) return;

        if (damage  > 30)
        {
            curEffect = effect.effects[(int)EffectType.HighAttack];
        }
        else
        {
            curEffect = effect.effects[(int)EffectType.LowAttack];
        }
        if (character.Enemy.Hp - damage  <= 0)
        {
            curEffect = effect.effects[(int)EffectType.FinalAttack];
        }
    }

    private IEnumerator Effect()
    {
        isEffect = true;
        curEffect?.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        curEffect?.gameObject.SetActive(false);
    }

    ~AttackState()
    {
        EventManager<int>.StopListening(Define.ON_END_ROLL, SetAttackEffect);
    }
}
