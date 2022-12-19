using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioClip zeroClip;

    [SerializeField]
    private AudioClip hitClip;

    private void Awake()
    {
        EventManager<int>.StartListening(Define.ON_END_DRAW, Damage);
        EventManager.StartListening(Define.ON_HIT, Hit);
    }

    private void Damage(int damage)
    {
        if (damage == 0)
        {
            SoundManager.Instance.PlayOneshot(zeroClip);
        }
    }

    private void Hit() => SoundManager.Instance.PlayOneshot(hitClip);

    private void OnDestroy()
    {
        EventManager<int>.StopListening(Define.ON_END_DRAW, Damage);
        EventManager.StopListening(Define.ON_HIT, Hit);
    }
}
