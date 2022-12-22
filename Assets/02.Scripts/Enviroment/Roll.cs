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

    [SerializeField]
    RotateMode rotmode;

    private void OnEnable()
    {
        transform.eulerAngles = rot;

        seq = DOTween.Sequence();

        seq.Append(transform.DORotate(rot, duration, rotmode));
        seq.AppendCallback(() => transform.eulerAngles = rot);
        seq.AppendInterval(stopInverval);

        seq.Append(transform.DORotate(rot2 + Vector3.one * 360f, duration, rotmode));
        seq.AppendCallback(() => transform.eulerAngles = rot2);
        seq.AppendInterval(stopInverval);

        seq.SetLoops(-1);
    }

    private void OnDisable()
    {
        seq.Kill();
    }
}
