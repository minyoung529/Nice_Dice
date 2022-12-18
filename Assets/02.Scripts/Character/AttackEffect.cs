using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum EffectType
{
    LowAttack,
    HighAttack,
    FinalAttack,
    Shield,
    Hit,
    Strong,
    HackedShield,
    Miss,
    Length
}

public class AttackEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject[] effects = new GameObject[(int)EffectType.Length];
    [SerializeField]
    private AudioClip[] sounds = new AudioClip[(int)EffectType.Length];

    bool isPlayer;

    private void Start()
    {
        isPlayer = GetComponent<Character>().IsPlayer;
    }

    public GameObject ActiveEffect(EffectType type)
    {
        int idx = (int)type;

        if (idx < effects.Length)
        {
            effects[idx]?.gameObject.SetActive(true);
        }
        if (idx < sounds.Length)
        {
            if (sounds[idx])
            {
                if (isPlayer)
                    SoundManager.Instance.PlayOneshot(sounds[idx], AudioType.Effect, 1f);
                else
                    SoundManager.Instance.PlayOneshot(sounds[idx], AudioType.Effect, 0.5f);
            }
        }

        return effects[idx];
    }
    public GameObject InactiveEffect(EffectType type)
    {
        int idx = (int)type;

        if (idx < effects.Length)
        {
            effects[idx]?.gameObject.SetActive(false);
            return effects[idx];
        }

        return null;
    }

    public GameObject GetEffect(EffectType type)
    {
        return effects[(int)type];
    }
}
