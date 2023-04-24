using System.Collections;
using System.Collections.Generic;
using RX;
using UnityEngine;

namespace Scrips.GameScene.Info
{
    public interface IBulletInfo
    {
        IObservable<(EnemyBook,int)> AddPoint { get; }
    }
}
