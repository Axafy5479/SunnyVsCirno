using System.Collections;
using System.Collections.Generic;
using DependencyInjection;
using Scrips.GameScene.Data;
using Scrips.GameScene.Info;
using UnityEngine;
using WB.DI;

namespace Scrips.GameScene.Command
{
    public class MakeEnemyCommand : CommandBase
    {
        private List<EnemyBook> Books { get; }
        public List<IEnemyInfo> EnemyInfos { get; private set; }

        public MakeEnemyCommand(List<EnemyBook> enemyBooks)
        {
            Books = enemyBooks;
        }
        public override void Run()
        {
            List<Enemy> enemies = new List<Enemy>();
          
            Books.ForEach(b => enemies.Add(new Enemy(b)));
            EnemyInfos = enemies.ConvertAll(e=>e as IEnemyInfo);
            WBDI.Get<SceneData>().NewScene(enemies);
        }
    }
}
