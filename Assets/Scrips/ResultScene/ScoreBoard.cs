using System.Collections;
using System.Collections.Generic;
using DependencyInjection;
using Scrips.GameScene.Info;
using UnityEngine;
using WB.DI;

public class ScoreBoard : MonoBehaviour
{
    private ISceneInfo Scene { get; set; }

    [Inject]
    private void Construct(ISceneInfo scene)
    {
        Scene = scene;
    }
    
    
    public void OnButtonClicked()
    {
        if (Setting.RUN_ON_ATUMARU)
        {
            int stageId = Scene.StageBook.StageId;
            Atsumaru.scoreboards.display((stageId+1).ToString());
        }
    }
}
