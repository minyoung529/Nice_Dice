using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingSkill : SkillBase
{
    public GameObject effect;
    [SerializeField]
    private AudioClip hackStart;

    protected override void OnActSkill()
    {
        base.OnActSkill();

        AttackEffect attack = character.Enemy.GetComponent<AttackEffect>();
        effect = Instantiate(effect, enemy.transform.position, Quaternion.identity);
        enemy.MustHit = true;

        GameManager.Instance.DamageWeight = 1f;
        GameManager.Instance.UI.ActiveEffectText("");

        if (attack.GetEffect(EffectType.Shield).activeSelf)
        {
            attack.InactiveEffect(EffectType.Shield);
            attack.ActiveEffect(EffectType.HackedShield);
            SoundManager.Instance.PlayOneshot(hackStart);
        }
    }

    protected override void OnNextTurn()
    {
        enemy.MustHit = false;
        Destroy(effect);
        Destroy(gameObject);
    }
}
