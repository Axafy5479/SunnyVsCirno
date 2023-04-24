using System.Collections;
using System.Collections.Generic;
using DependencyInjection;
using RX;
using Scrips.GameScene.Info;
using UnityEngine;
using WB.DI;

namespace Scrips.GameScene.Data
{
    public class BulletData:IBulletInfo
    {
        public List<Enemy> DefeatedEnemy = new List<Enemy>();
        private Subject<(EnemyBook,int)> addPoint = new Subject<(EnemyBook,int)>((null,0));
        public IObservable<(EnemyBook,int)> AddPoint => addPoint;
        public void AddEnemy(Enemy enemy)
        {

            float ratio = Mathf.Pow(Setting.MULTIPLE_DEFEAT_SCORE_RATIO, DefeatedEnemy.Count);

            if (WBDI.Get<ISceneInfo>().SceneEffects.Contains(SceneEffect.LunaticTime))
            {
                ratio *= 2;
            }
            
            int point = (int) (enemy.Book.Point * ratio);
            addPoint.OnNext((enemy.Book,point));
            WBDI.Get<PlayerData>().AddDefeatedEnemy(enemy,point);
            
            DefeatedEnemy.Add(enemy);

        }
        
    }
}
