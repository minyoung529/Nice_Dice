using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlockSkill : SkillBase
{
    public GameObject chain;
    int turn = 0;

    protected override void OnStart()
    {
        character.Enemy.IsBlock = true;
    }

    protected override void OnNextTurn()
    {
        if (turn++ > 0)
        {
            Destroy(chain);
            Destroy(gameObject);
        }
    }

    protected override void OnActSkill()
    {
        if(turn == 0)
        {
            GameManager.Instance.MainCam.transform.DOShakePosition(0.5f);

            Vector3 pos = character.Enemy.transform.position;
            pos.y = -0.7f;
            pos.x = 0.6f;

            chain = Instantiate(chain, pos, Quaternion.identity, null);
            chain.transform.localScale = Vector3.one * 30f;

            chain.transform.DOScale(Vector3.one, 1f).SetEase(Ease.InFlash).OnComplete(() =>
            {
                SoundManager.Instance.PlayOneshot(activeSkillClip);
            });
        }
    }
}
