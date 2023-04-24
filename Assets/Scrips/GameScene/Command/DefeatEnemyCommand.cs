using System.Collections;
using System.Collections.Generic;
using DependencyInjection;
using Scrips.GameScene.Data;
using Scrips.GameScene.Info;
using UnityEngine;
using WB.DI;

namespace Scrips.GameScene.Command
{
    public class DefeatEnemyCommand : CommandBase
    {
        public DefeatEnemyCommand(IBulletInfo bullet,IEnemyInfo enemyInfo)
        {
            Bullet = bullet;
            EnemyInfo = enemyInfo;
        }
        
        public IBulletInfo Bullet { get; }
        public IEnemyInfo EnemyInfo { get; }
        
        public bool Killed { get;private set; }
        
        public override void Run()
        {
            if ((EnemyInfo as Enemy).Crash())
            {
                (Bullet as BulletData).AddEnemy(EnemyInfo as Enemy);
                WBDI.Get<SceneData>().Remove(EnemyInfo as Enemy);
                WBDI.Get<SceneData>().Remove(EnemyInfo as Enemy);
                Killed = true;
            }
            else
            {
                Killed = false;
            }
        }
    }
}
