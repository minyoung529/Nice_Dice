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
    AudioSource[] audios = new AudioSource[3];

    private bool mute = false;
    public bool Mute
    {
        get { return mute; }
        set
        {
            mute = value;

            if (mute)
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
                    audio.volume = 1f;
                }
            }
        }
    }

    protected override void Awake()
    {
        FirstSetting();
    }

    public void PlayOneshot(AudioClip clip, AudioType audio = AudioType.Effect, float volume = 1f)
    {
        if (!clip) return;

        FirstSetting();
        audios[(int)audio].PlayOneShot(clip, volume);
    }

    public void Play(AudioType audio, AudioClip clip)
    {
        if (!clip) return;

        FirstSetting();
        audios[(int)audio].clip = clip;
        audios[(int)audio].Play();
        Debug.Log("PLAYER_BGM");
    }

    public void Stop(AudioType audio)
    {
        FirstSetting();
        audios[(int)audio].Stop();
    }

    private void FirstSetting()
    {
        if (audios[0] == null)
        {
            audios[0] = gameObject.AddComponent<AudioSource>();
            audios[1] = gameObject.AddComponent<AudioSource>();
            audios[2] = gameObject.AddComponent<AudioSource>();

            audios[0].loop = true;
            //Mute = false;
        }
    }
}
