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

    private bool mute = false;
    public bool Mute
    {
        get { return mute; }
        set
        {
            mute = value;

            if(mute)
            {
                foreach (AudioSource audio in audios)
                {
                    audio.volume = 0f;
                }
            }
            else
            {
                foreach (AudioSource audio in audios)
                {
                    audio.volume = 0.5f;
                }
            }
        }
    }

    protected override void Awake()
    {
        audios[0] = gameObject.AddComponent<AudioSource>();
        audios[1] = gameObject.AddComponent<AudioSource>();
        audios[2] = gameObject.AddComponent<AudioSource>();

        Mute = false;
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
