using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Roll : MonoBehaviour
{
    [SerializeField] private float rotSpeed = 10f;
    private Sequence seq;

    private readonly float duration = 0.5f;
    private readonly float stopInverval = 3f;

    private Vector3 rot = new Vector3(-22.5f, 45f, -22.5f);
    private Vector3 rot2 = new Vector3(19.5f, 127f, 156f);

    private void Start()
    {
        seq = DOTween.Sequence();

        seq.Append(transform.DORotate(rot, duration, RotateMode.FastBeyond360));
        seq.AppendInterval(stopInverval);

        seq.Append(transform.DORotate(rot2, duration, RotateMode.FastBeyond360));
        seq.AppendInterval(stopInverval);

        seq.SetLoops(-1);
    }
}
