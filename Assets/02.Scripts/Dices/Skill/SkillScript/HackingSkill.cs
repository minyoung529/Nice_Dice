using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingSkill : SkillBase
{
    public GameObject effect; 

    protected override void OnActSkill()
    {
        effect = Instantiate(effect, enemy.transform.position, Quaternion.identity);
        enemy.MustHit = true;
    }

    protected override void OnNextTurn()
    {
        enemy.MustHit = false;
        Destroy(effect);
        Destroy(gameObject);
    }
}
