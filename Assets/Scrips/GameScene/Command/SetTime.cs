using System.Collections;
using System.Collections.Generic;
using DependencyInjection;
using Scrips.GameScene.Data;
using UnityEngine;
using WB.DI;

public class SetTime
{
    #region Singleton

    private static SetTime instance;
    public static SetTime I => instance ??= new SetTime();
    private SetTime(){}
    #endregion

    public void SetTimeToData(float remainTime)
    {
        var sceneInfo = WBDI.Get<SceneData>();
        sceneInfo.RemainTime = remainTime;
    }


}
