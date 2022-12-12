using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

/// <summary>
/// 맞는 상태
/// </summary>
public class HitState : StateBase
{
    private readonly int hitHash = Animator.StringToHash("Hit");
    private float timer;
    private readonly int BASE_COLOR = Shader.PropertyToID("_BaseColor");
    private readonly int SHADER_COLOR = Shader.PropertyToID("_1st_ShadeColor");

    private Renderer renderer;
    private int damage = 0;

    public HitState(Character character) : base(character) { }

    public override void OnAwake()
    {
        renderer = character.GetComponentInChildren<Renderer>();
        EventManager<int>.StartListening(Define.ON_END_ROLL, SetDamage);
    }

    public override void OnStart()
    {
        character.Animator.SetTrigger(hitHash);
        timer = character.Animator.GetCurrentAnimatorClipInfo(0).Length;

        character.StartCoroutine(HitEffect());

        if (GameManager.Instance.PlayerTurn != character.IsPlayer)
        {
            character.Hp -= damage;
        }
    }

    private IEnumerator HitEffect()
    {
        Color oldBaseColor = renderer.material.GetColor(BASE_COLOR);
        Color oldShaderColor = renderer.material.GetColor(SHADER_COLOR);

        renderer.material.SetColor(BASE_COLOR, Color.red);
        renderer.material.SetColor(SHADER_COLOR, Color.red);

        yield return new WaitForSeconds(0.6f);

        renderer.material.SetColor(BASE_COLOR, oldBaseColor);
        renderer.material.SetColor(SHADER_COLOR, oldShaderColor);
    }


    public override void OnUpdate()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            character.ChangeState(CharacterState.Idle);
            GameManager.Instance.NextTurn();
        }
    }

    public override void OnEnd()
    {
        damage = 0;
    }

    public void SetDamage(int _damage)
    {
        if (!character.IsPlayer)
        {
            AIEnemyController enemy = character?.GetComponent<AIEnemyController>();
            switch (enemy.monsterData.MonsterType)
            {
                case MonsterType.Odd:
                    if (_damage % 2 != 1)
                        _damage = 0;
                    break;
                case MonsterType.Even:
                    if (_damage % 2 != 0)
                        _damage = 0;
                    break;
                case MonsterType.Range:
                    if (_damage < enemy.monsterData.MinDamage || _damage > enemy.monsterData.MaxDamage)
                        _damage = 0;
                    break;
                case MonsterType.Unknown:
                    break;
                default:
                    break;
            }
        }

        damage = _damage;
    }

    public override void OnDestroy()
    {
        EventManager<int>.StopListening(Define.ON_END_ROLL, SetDamage);
    }
}