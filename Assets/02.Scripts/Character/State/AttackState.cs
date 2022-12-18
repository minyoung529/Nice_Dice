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
    private EffectType effectType;
    int damage = 0;

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
        this.damage = damage;

        damage = (int)(damage * GameManager.Instance.DamageWeight);

        if (character.Enemy.Hp - damage <= 0)
        {
            effectType = EffectType.FinalAttack;
        }
        else if (damage > 30)
        {
            effectType = EffectType.HighAttack;
        }
        else if (damage > 0)
        {
            effectType = EffectType.LowAttack;
        }
    }

    private IEnumerator Effect()
    {
        isEffect = true;

        if (damage == 0)
        {
            effect.ActiveEffect(EffectType.Miss);
            Debug.Log("MISS");
        }
        else
            effect.ActiveEffect(effectType);

        yield return new WaitForSeconds(1f);

        effect.InactiveEffect(effectType);
        effect.InactiveEffect(EffectType.Miss);
    }

    ~AttackState()
    {
        EventManager<int>.StopListening(Define.ON_END_ROLL, SetAttackEffect);
    }
}
