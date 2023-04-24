using System.Collections;
using System.Collections.Generic;
using DependencyInjection;
using Scrips.GameScene.Command;
using Scrips.GameScene.Data;
using UnityEngine;
using WB.DI;

namespace Scrips.GameScene.Command
{


    public class GameSetCommand : CommandBase
    {
        public override void Run()
        {
            WBDI.Get<SceneData>().OnGameSet();
        }
    }
}
