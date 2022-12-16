using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : StateBase
{
    readonly int hashDie = Animator.StringToHash("Die");
    private float timer = 0f;
    private bool isCallOnce = true;

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
            timer = 0f;
            OnEnd();
        }
    }

    public override void OnEnd()
    {
        if (!isCallOnce) { return; }
        isCallOnce = false;

        if (character.CompareTag("Player")) // 霸烙 场
        {
            GameManager.Instance.UI.WinUI.UpdateUI(true, GameManager.Instance.Enemy.GetComponent<AIEnemyController>().monsterData.MonsterName, GameManager.maxDeal);
            EventManager.TriggerEvent(Define.ON_END_GAME);
        }
        else
        {
            Object.Destroy(character.gameObject);
            // 货肺款 局
            GameManager.Instance.stage++;
            EventManager.TriggerEvent(Define.ON_NEXT_STAGE);
            GameManager.Instance.UI.HeaderUI.UpdateUI();
            GameManager.Instance.NextTurn();
            GameManager.Instance.NextTurn();
        }

        isCallOnce = true;
    }
}
