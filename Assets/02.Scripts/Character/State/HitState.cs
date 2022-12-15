using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using static UnityEngine.Rendering.DebugUI;

/// <summary>
/// �´� ����
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
        timer = /*character.Animator.GetCurrentAnimatorClipInfo(0).Length*/2f;

        character.StartCoroutine(HitEffect());
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

        if (GameManager.Instance.PlayerTurn != character.IsPlayer)
        {
            character.Hp -= damage;

            if (character.Hp <= 0)
            {
                character.ChangeState(CharacterState.Die);
            }
        }
    }

    public override void OnUpdate()
    {
        timer -= Time.deltaTime;

        if (timer < 0 && character.Hp > 0)
        {
            character.ChangeState(CharacterState.Idle);

            if (character.IsBlock)
            {
                GameManager.Instance.PlayerTurn = !GameManager.Instance.PlayerTurn;
                character.IsBlock = false;
            }

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
            if (character == null) { character = GameManager.Instance.Enemy; }

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