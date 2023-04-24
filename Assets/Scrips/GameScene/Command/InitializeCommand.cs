using System.Collections;
using System.Collections.Generic;
using DependencyInjection;
using Scrips.GameScene.Data;
using UnityEngine;
using WB.DI;

namespace Scrips.GameScene.Command
{
    public class InitializeCommand : CommandBase
    {
        public StageBook StageBook { get; }
        public InitializeCommand(StageBook stageBook)
        {
            StageBook = stageBook;
        }
        public override void Run()
        {
            WBDI.Get<SceneData>().Initialize(StageBook);
            WBDI.Get<PlayerData>().Reset();

        }
    }
}
