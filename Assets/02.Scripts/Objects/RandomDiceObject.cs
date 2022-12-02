using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RandomDiceObject : MonoBehaviour
{
    public void Init(Transform[] positions, Transform lastPos)
    {
        Sequence seq = DOTween.Sequence();

        for (int i = 0; i < positions.Length; i++)
        {
            seq.Append(transform.DOMove(positions[i].position, 0.15f)).SetEase(Ease.Linear);
        }

        //seq.Append(transform.DOMove(lastPos.position, 0.7f));
        //seq.AppendCallback(() => Destroy(gameObject));
    }
}
