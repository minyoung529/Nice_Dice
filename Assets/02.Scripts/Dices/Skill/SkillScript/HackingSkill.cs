using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingSkill : SkillBase
{
    public GameObject effect; 

    protected override void OnActSkill()
    {
        AttackEffect attack = character.Enemy.GetComponent<AttackEffect>();
        effect = Instantiate(effect, enemy.transform.position, Quaternion.identity);
        enemy.MustHit = true;

        if (attack.effects[(int)EffectType.Shield].activeSelf)
        {
            attack.effects[(int)EffectType.Shield].SetActive(false);
            attack.effects[(int)EffectType.HackedShield].SetActive(true);
        }
    }

    protected override void OnNextTurn()
    {
        enemy.MustHit = false;
        Destroy(effect);
        Destroy(gameObject);
    }
}
