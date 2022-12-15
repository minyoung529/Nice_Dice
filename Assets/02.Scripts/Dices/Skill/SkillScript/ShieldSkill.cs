using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSkill : SkillBase
{
    int turn = 0;
    private AttackEffect effect;

    protected override void OnStart()
    {
        effect = character.GetComponent<AttackEffect>();
        EventManager<int>.StartListening(Define.ON_END_ROLL, OnEndRoll);
    }

    protected override void OnNextTurn()
    {
        if (turn++ <= 0)
        {
            GameManager.Instance.DamageWeight -= 0.5f;
        }
        else
        {
            // TODO: 고쳐야한다!
            GameManager.Instance.DamageWeight += 0.5f;

            effect.effects[(int)EffectType.Shield].gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void OnEndRoll(int damage)
    {
            GameManager.Instance.UI.ActiveEffectText("/2");
        //GameManager.Instance.UI.ActiveEffectText($"/2 = {(int)(damage  * GameManager.Instance.DamageWeight)}");
    }

    protected override void OnActSkill()
    {
        if (turn == 0)
        {
            effect.effects[(int)EffectType.Shield].gameObject.SetActive(true);
        }
    }

    protected override void ChildOnDestroy()
    {
        EventManager<int>.StopListening(Define.ON_END_ROLL, OnEndRoll);
    }
}
