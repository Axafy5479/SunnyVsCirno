using Scrips.GameScene.Data;
using Scrips.GameScene.Info;
using UnityEngine;
using WB.DI;

namespace DependencyInjection
{
    public class DiContainerInitializer:ContainerInstaller
    {

        protected override void ContainerInstall(IBinder binder)
        {
            var player = new PlayerData();
            var scene = new SceneData();
            binder.BindSingleton(player);
            binder.BindSingleton<IPlayerInfo>(player);
            binder.BindSingleton(scene);
            binder.BindSingleton<ISceneInfo>(scene);
        }
    }
}
