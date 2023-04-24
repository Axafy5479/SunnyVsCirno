using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RX;

namespace Scrips.GameScene.Info
{
    public interface ISceneInfo
    {
        StageBook StageBook { get; }
        
        ReadOnlyCollection<IEnemyInfo> Enemies { get; }

        ReadOnlyCollection<SceneEffect> SceneEffects { get; }

        IObservable<NoMean> NextScene { get; }
        
        IObservable<NoMean> GameSet { get; }

        float RemainTime { get; }
    }
}
