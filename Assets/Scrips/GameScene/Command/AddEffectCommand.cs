using System.Collections;
using System.Collections.Generic;
using DependencyInjection;
using Scrips.GameScene.Command;
using Scrips.GameScene.Data;
using Scrips.GameScene.Info;
using UnityEngine;
using WB.DI;

public class AddEffectCommand : CommandBase
{
    public SceneEffect Effect { get; }
    public AddEffectCommand(SceneEffect effect)
    {
        Effect = effect;
    }
    public override void Run()
    {
        WBDI.Get<SceneData>().AddEffect(Effect);
    }
}
