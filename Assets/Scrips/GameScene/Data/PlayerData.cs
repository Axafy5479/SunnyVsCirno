using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RX;
using Scrips.GameScene.Info;
using UnityEngine;
using WB.DI;


namespace Scrips.GameScene.Data
{
    public class PlayerData:IPlayerInfo
    {

        private List<Enemy> _DefeatedEnemies = new List<Enemy>();

        [Inject]
        public PlayerData()
        {
            Reset();
        }

        public ReadOnlyCollection<IEnemyInfo> DefeatedEnemies 
            => _DefeatedEnemies.ConvertAll(e => e as IEnemyInfo).AsReadOnly();
        private Subject<int> _Score { get; set; } = new Subject<int>(0);
        public IObservable<int> Score => _Score;

        public void AddDefeatedEnemy(Enemy enemy,int point)
        {
            _DefeatedEnemies.Add(enemy);
            _Score.OnNext(Score.Value+point);
        }

        public void Reset()
        {
            _DefeatedEnemies.Clear();
            _Score = new Subject<int>(0);
        }

    }
}
