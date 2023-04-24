using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RX;
using Scrips.GameScene.Info;
using UnityEngine;

public interface IPlayerInfo
{
    IObservable<int> Score { get; }
    ReadOnlyCollection<IEnemyInfo> DefeatedEnemies { get; }
}
