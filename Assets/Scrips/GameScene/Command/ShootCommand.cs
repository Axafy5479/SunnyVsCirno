using System.Collections;
using System.Collections.Generic;
using Scrips.GameScene.Command;
using Scrips.GameScene.Data;
using Scrips.GameScene.Info;
using UnityEngine;

namespace Scrips.GameScene.Command
{
    public class ShootCommand : CommandBase
    {
        public IBulletInfo Bullet { get; private set; }
        public override void Run()
        {
            Bullet = new BulletData();
        }
    }
}
