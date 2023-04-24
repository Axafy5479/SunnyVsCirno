using System.Collections;
using System.Collections.Generic;
using DependencyInjection;
using Scrips.GameScene.Data;
using Scrips.GameScene.Info;
using UnityEngine;
using WB.DI;

namespace Scrips.GameScene.Command
{
    public class RunAwayCommand : CommandBase
    {
        public RunAwayCommand(IEnemyInfo enemyInfo)
        {
            EnemyInfo = enemyInfo;
        }
        
        public IEnemyInfo EnemyInfo { get; }
        
        public override void Run()
        {
            WBDI.Get<SceneData>().Remove(EnemyInfo as Enemy);
        }
    }
}
