using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : StateBase
{
    readonly int hashDie = Animator.StringToHash("Die");
    private float timer = 0f;

    public DieState(Character character) : base(character)
    {
    }

    public override void OnStart()
    {
        character.Animator.SetTrigger(hashDie);
    }

    public override void OnUpdate()
    {
        timer += Time.deltaTime;

        if (timer > 1.5f)
        {
            OnEnd();
        }
    }

    public override void OnEnd()
    {
        if (character.CompareTag("Player")) // ���� ��
        {
            GameManager.Instance.UI.WinUI.UpdateUI(true, "���� ����", GameManager.maxDeal);
            EventManager.TriggerEvent(Define.ON_END_GAME);
        }
        else
        {
            Object.Destroy(character.gameObject);
            // ���ο� ��
        }
    }
}