using System.Collections;
using System.Collections.Generic;
using RX;
using UnityEngine;

namespace Scrips.GameScene.Info
{
    public interface IEnemyInfo
    {
        EnemyBook Book { get; }
        IObservable<int> Hp { get; }
    }
}
