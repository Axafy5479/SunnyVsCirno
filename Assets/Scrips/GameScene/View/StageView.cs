using System;
using System.Collections;
using System.Collections.Generic;
using RX;
using Scrips.GameScene.Command;
using Scrips.GameScene.Info;
using UnityEngine;
using WB.DI;
using WBTransition;

namespace Scrips.GameScene.View
{
    public class StageView : MonoSingleton<StageView>,ISceneInitializer
    {
        private StageBook stageBook;
        [SerializeField] private GameObject readyPrefab;
        [SerializeField] private Scene resultScene;
        
        private GameObject currentStage;
        public int StageNum { get; private set; }
        public Subject<bool> IsRunning { get; } = new Subject<bool>(false);
        private ISceneInfo Scene { get; set; }
        private IDisposable sceneDataMonitor;
        private IDisposable gameSetMonitor;

        [Inject]
        private void Construct(ISceneInfo scene)
        {
            Scene = scene;
        }
        
        
        private void Ready(bool bgmContinue)
        {
            Transform readyTrn = GameObject.Find("Canvas").transform;
            Ready ready = Instantiate(readyPrefab,readyTrn).GetComponent<Ready>();

            ready.Show(new List<(string, float)>(){("れでぃー？",0.7f),("せっと",0.7f),("せんせんふこく!",0.7f)},
                ()=>StartCoroutine(_makeScene(bgmContinue)));
        }

        private IEnumerator _makeScene(bool bgmContinue)
        {
            yield return MakeScene(0);
            IsRunning.OnNext(true);
            TimeManager.I.GameStart(stageBook.LimitTime);
            if (!bgmContinue)
            {
                SoundManager.I.PlayBGM(stageBook.BGM);
            }
        }
        
        private void GameSet()
        {
            IsRunning.OnNext(false);
   
            SEPlayerAssist.I.Play(SEType.WHISTLE);

            Transform readyTrn = GameObject.Find("Canvas").transform;
            Ready ready = Instantiate(readyPrefab,readyTrn).GetComponent<Ready>();
            
            ready.Show(new List<(string, float)>(){("そこまで!",2f)}, 
                ()=>SceneManager.LoadScene(resultScene));
        }

        private IEnumerator MakeScene(int stageNumber)
        {
            yield return new WaitForSeconds(0.5f);
            currentStage = Instantiate(stageBook.StagePrefabs[stageNumber], this.transform);
            List<EnemyView> enemyViews = new List<EnemyView>();
            foreach (Transform trn in currentStage.transform)
            {
                if (trn.GetComponent<EnemyView>() is { } ev)
                {
                    enemyViews.Add(ev);
                }
            }

            var command = new MakeEnemyCommand(enemyViews.ConvertAll(e=>e.Book));
            command.Run();
            for (int i = 0; i < enemyViews.Count; i++)
            {
                enemyViews[i].EnemyInfo = command.EnemyInfos[i];
            }
        }

        public IEnumerator BeforeOpenMask(Dictionary<string, object> args)
        {
            stageBook = (StageBook)args["stageBook"];
            if (Setting.RUN_ON_ATUMARU)
            {
                Atsumaru.Comment.ChangeScene("Stage"+stageBook.StageId);
            }
            new InitializeCommand(stageBook).Run();

            sceneDataMonitor = Scene.NextScene.Subscribe(_ =>
            {
                Destroy(currentStage);

                StageNum++;
                if (StageNum < stageBook.StagePrefabs.Length)
                {
                    StartCoroutine(MakeScene(StageNum));
                }
                else
                {
                    new GameSetCommand().Run();
                }
            });
            yield return null;
        }

        public void AfterOpenMask(Dictionary<string, object> args)
        {
            gameSetMonitor = Scene.GameSet.Subscribe(_ => GameSet());
            Ready((bool)args["bgmContinue"]);
        }

        private void OnDestroy()
        {
            sceneDataMonitor.Dispose();
            gameSetMonitor.Dispose();
        }
    }
}
