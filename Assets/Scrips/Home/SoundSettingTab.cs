using System;
using System.Collections;
using System.Collections.Generic;
using Scrips.GameScene.Info;
using UnityEngine;

public class SoundSettingTab : HomeTabBase
{
    [SerializeField] private VolumeSettingSliders volumeSetter;

    protected override float HideY => 373.85f;
    protected override float ShowY => 0f;

    protected override void OnShow()
    {

        volumeSetter.StartSetting();
        
    }
    
    protected override void OnHide()
    {
        //volumeSetter.EndSetting();
    }
    


}
