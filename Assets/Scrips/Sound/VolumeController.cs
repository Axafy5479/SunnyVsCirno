using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private AudioClip seSettingClip;
    SoundManager Manager => SoundManager.I;
    public float BGMVolume { get;internal set; }
    public float SEVolume { get;internal set; }
    private IAudioSource _audioSource;

    private void Start()
    {
        BGMVolume = Manager.BgmVolume;
        SEVolume = Manager.SeVolume;
    }

    private void Update()
    {
        Manager.SetSeVolume(SEVolume);
        Manager.SetBGMVolume(BGMVolume);
    }


    public void PlaySeSettingClip()
    {
        if (!(_audioSource is {IsPlaying: true}))
        {
            _audioSource = SEPlayerAssist.I.Play(SEType.DEATH);
            _audioSource.IsLooping = false;
        }
    }

}
