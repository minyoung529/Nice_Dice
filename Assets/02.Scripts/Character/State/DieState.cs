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
        if (character.CompareTag("Player")) // 게임 끝
        {
            GameManager.Instance.UI.WinUI.UpdateUI(true, GameManager.Instance.Enemy.GetComponent<AIEnemyController>().monsterData.MonsterName, GameManager.maxDeal);
            EventManager.TriggerEvent(Define.ON_END_GAME);
        }
        else
        {
            Object.Destroy(character.gameObject);
            // 새로운 애
            GameManager.Instance.stage++;
            EventManager.TriggerEvent(Define.ON_NEXT_STAGE);
            GameManager.Instance.UI.HeaderUIController.UpdateUI();
        }
    }
}
