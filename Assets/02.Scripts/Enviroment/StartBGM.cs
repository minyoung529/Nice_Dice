using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBGM : MonoBehaviour
{
    [SerializeField]
    private AudioClip bgmClip;

    private void Start()
    {
        SoundManager.Instance.Play(AudioType.BGM, bgmClip);
    }
}
