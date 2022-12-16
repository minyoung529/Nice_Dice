using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSkill : SkillBase
{
    AttackEffect effect;

    protected override void OnStart()
    {
        effect = character.GetComponent<AttackEffect>();
        effect?.effects[(int)EffectType.Strong].gameObject.SetActive(true);

        GameManager.Instance.UI.ActiveEffectText("x 1.2");

        GameManager.Instance.DamageWeight += 0.2f;
    }

    protected override void OnNextTurn()
    {
        GameManager.Instance.DamageWeight -= 0.2f;
        effect?.effects[(int)EffectType.Strong].gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
