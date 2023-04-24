using System.Collections;
using System.Collections.Generic;
using DependencyInjection;
using Scrips.GameScene.Info;
using UnityEngine;
using WB.DI;
using WBTransition;

public class RetryButton : MonoBehaviour
{
   private ISceneInfo Scene { get; set; }
   
   [Inject]
   private void Construct(ISceneInfo scene)
   {
      Scene = scene;
   }
   
   public void Retry()
   {
      var sendingData = new Dictionary<string, object>()
      {
         {"stageBook", Scene.StageBook},
        
      };
      var sendingDataAfter = new Dictionary<string, object>()
      {
         {"bgmContinue",true}
      };
      
      
      SceneManager.LoadScene("GameScene",sendingData,sendingDataAfter);
   }

   public void Hide()
   {
      Destroy(this.gameObject);
   }
}
