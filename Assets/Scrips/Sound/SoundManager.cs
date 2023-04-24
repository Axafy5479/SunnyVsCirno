using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WB.Animation;

public class SoundManager : MonoBehaviour
{
    
    #region Singleton

    protected static SoundManager instance;

    public static SoundManager I
    {
        get
        {
            if (instance == null)
            {
                var temp = FindObjectsOfType<SoundManager>();
                if (temp.Length == 0)
                {
                    instance = new GameObject("SoundManager2").AddComponent<SoundManager>();
                    instance.OnInstantiate();
                }
                else if (temp.Length > 1)
                {
                    Debug.LogError("SoundManagerが複数見つかりました");
                    return null;
                }
                else
                {
                    instance = temp[0];
                }
            }

            return instance;
        }
    }

    #endregion

    public IAudioSource PlayBGM(AudioClip clip)
    {
        BgmSource.Play(clip,BgmVolume);
        return BgmSource;
    }
    public IAudioSource PlaySE(AudioClip clip)
    {
        var source = SeSources.Dequeue();
        source.Play(clip,SeVolume);
        SeSources.Enqueue(source);
        return source;
    }

    public IAudioSource PlayChargeSE(AudioClip clip1, AudioClip clip2)
    {
        var source = PlaySE(clip1) as SoundInnerClass;

        var s = new Sequence()
            .Append(new Delay(clip1.length))
            .Append(new CallbackMethod(() =>
            {
                source.Play(clip2,SeVolume);
            }));
        source.OnStop = s.Kill;
        s.Play();
        return source;
    }
    public float SeVolume { get;private set; }
    public float BgmVolume { get; private set; }
    
    private Queue<SoundInnerClass> SeSources { get; } = new Queue<SoundInnerClass>();
    private SoundInnerClass BgmSource{ get; set; }
    

    private void OnInstantiate()
    {
        DontDestroyOnLoad(this.gameObject);
        for (int i = 0; i < 5; i++)
        {
            SeSources.Enqueue(new SoundInnerClass(this.gameObject.AddComponent<AudioSource>()));
        }

        BgmSource = new SoundInnerClass(this.gameObject.AddComponent<AudioSource>());
        SeVolume = SaveSystem.I.UserData.SeVolume;
        BgmVolume = SaveSystem.I.UserData.BGMVolume;
    }
    
    public void SetSeVolume(float vol)
    {
        SeVolume = vol;
        foreach (var s in SeSources)
        {
            s.Volume = vol;
        }
    }

    public void SetBGMVolume(float vol)
    {
        BgmVolume = vol;
        BgmSource.Volume = vol;
    }

    public void BGMFade()
    {
        BgmSource.FadeOut(1);
    }

    private class SoundInnerClass : IAudioSource
    {
        public SoundInnerClass(AudioSource audioSource)
        {
            AudioSource = audioSource;
        }
        
        private AudioSource AudioSource { get; }
        public Action OnStop { get; set; }

        private Sequence fadeSequence;
        
        public void Stop()
        {
            OnStop?.Invoke();
            AudioSource.Stop();
            OnStop = null;
        }

        public void Play(AudioClip clip,float volume)
        {
            if (fadeSequence == null)
            {
                _play(clip,volume);
            }
            else
            {
                fadeSequence.Append(new CallbackMethod(() => _play(clip, volume)));
            }
        }
        
        public float Volume
        {
            get => AudioSource.volume;
            set => AudioSource.volume = value;
        }

        private void _play(AudioClip clip,float volume)
        {
            AudioSource.clip = clip;
            AudioSource.volume = volume;
            AudioSource.Play();
        }

        public void FadeOut(float fadeTime)
        {
            fadeSequence = new Sequence()
                .Append(new GeneralAnim(AudioSource.volume, 0, x => AudioSource.volume = x, fadeTime))
                .Append(new CallbackMethod(() =>
                {
                    Stop();
                    fadeSequence = null;
                }));
            fadeSequence.Play();
        }

        public bool IsLooping { get=>AudioSource.loop; set=>AudioSource.loop = value; }
        public bool IsPlaying => AudioSource.isPlaying;
    }
}


