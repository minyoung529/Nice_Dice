using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : StateBase
{
    readonly int hashDie = Animator.StringToHash("Die");
    private float timer = 0f;
    private bool isCallOnce = true;
    private bool delayCmp;

    public DieState(Character character) : base(character)
    {
    }

    public override void OnStart()
    {
        character.Animator.SetTrigger(hashDie);
        timer = 0f;
        //isCallOnce = false;
    }

    public override void OnUpdate()
    {
        timer += Time.deltaTime;

        if (timer > 3f)
        {
            timer = 0f;
            character.ChangeState(CharacterState.Idle);
        }
    }

    public override void OnEnd()
    {
        //if (!isCallOnce) { return; }
        //if (!delayCmp) return;

        //isCallOnce = false;

        if (character.CompareTag("Player")) // 게임 끝
        {
            GameManager.Instance.UI.WinUI.UpdateUI();
            EventManager.TriggerEvent(Define.ON_END_GAME);
        }
        else
        {
            character.Release();
            Object.Destroy(character.gameObject);
            // 새로운 애
            GameManager.Instance.stage++;
            EventManager.TriggerEvent(Define.ON_NEXT_STAGE);
            GameManager.Instance.UI.HeaderUI.UpdateUI();
            
            GameManager.Instance.Player.Hp += 10;
            if(GameManager.Instance.Player.Hp> GameManager.Instance.Player.MaxHp)
            {
                GameManager.Instance.Player.Hp = GameManager.Instance.Player.MaxHp;
            }

            AIEnemyController controller = character.GetComponent<AIEnemyController>();
            GameManager.Instance.ClearMonsters.Add(controller.monsterData.MonsterName);
        }

        //isCallOnce = true;
    }
}
