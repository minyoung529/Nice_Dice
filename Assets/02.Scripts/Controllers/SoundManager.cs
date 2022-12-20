using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioType
{
    BGM, Environment, Effect, Length
}

public class SoundManager : MonoSingleton<SoundManager>
{
    AudioSource[] audios = new AudioSource[(int)AudioType.Length];

    protected override void Awake()
    {
        audios[0] = gameObject.AddComponent<AudioSource>();
        audios[0].volume = 0f;

        audios[1] = gameObject.AddComponent<AudioSource>();
        audios[2] = gameObject.AddComponent<AudioSource>();
    }

    public void PlayOneshot(AudioClip clip, AudioType audio = AudioType.Effect, float volume = 1f)
    {
        if (!clip) return;

        audios[(int)audio].PlayOneShot(clip, volume);
    }

    public void Play(AudioType audio, AudioClip clip)
    {
        if (!clip) return;

        audios[(int)audio].clip = clip;
        audios[(int)audio].Play();
    }

    public void Stop(AudioType audio)
    {
        audios[(int)audio].Stop();
    }
}
