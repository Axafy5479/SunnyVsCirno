using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SEType
{
    WADAIKO,KILL,DEATH,ANGRY,POKA,BlockBrake,WHISTLE,CHARGE,SHOOT
}

public class SEPlayerAssist
{
    #region Singleton
    private static SEPlayerAssist instance;
    public static SEPlayerAssist I => instance ??= new SEPlayerAssist();

    private SEPlayerAssist()
    {
       

        AudioClip charge1 = Resources.Load<AudioClip>("SE/Charge1");
        AudioClip charge2 = Resources.Load<AudioClip>("SE/Charge2");
        AudioClip don = Resources.Load<AudioClip>("SE/どん_nc10485");
        AudioClip kill = Resources.Load<AudioClip>("SE/銃声_nc25721");
        AudioClip shoot = Resources.Load<AudioClip>("SE/Shoot");
        AudioClip death = Resources.Load<AudioClip>("SE/Death");
        AudioClip angry = Resources.Load<AudioClip>("SE/ブチッnc70890");
        AudioClip poka = Resources.Load<AudioClip>("SE/ポカッnc145057");
        AudioClip blockBrake = Resources.Load<AudioClip>("SE/BlockBrake");
        AudioClip whistle = Resources.Load<AudioClip>("SE/ホイッスルnc143362");

        

        ClipMap = new Dictionary<SEType, AudioClip[]>()
        {
            {SEType.KILL,new []{kill}},
            {SEType.WADAIKO,new []{don}},
            {SEType.DEATH,new []{death}},
            {SEType.ANGRY,new []{angry}},
            {SEType.POKA,new []{poka}},
            {SEType.BlockBrake,new []{blockBrake}},
            {SEType.WHISTLE,new []{whistle}},
            {SEType.CHARGE,new []{charge1,charge2}},
            {SEType.SHOOT,new []{shoot}}
        };
    }
    #endregion

    public IAudioSource Play(SEType se)
    {
        var clips = ClipMap[se];
        switch (clips.Length)
        {
            case 1:
                return SoundManager.I.PlaySE(clips[0]);
            case 2:
                return SoundManager.I.PlayChargeSE(clips[0],clips[1]);
            default:
                throw new NotImplementedException();
        }
            
    }

    // public void PlaySingle(SEType se)
    // {
    //     if (SoundManager2.I.IsSePlaying)
    //     {
    //         return;
    //     }
    //     else
    //     {
    //         Play(se);
    //     }
    // }


    private Dictionary<SEType,AudioClip[]> ClipMap { get; }
}
