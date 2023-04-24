using Scrips.Animation;
using Scrips.GameScene.Command;
using Scrips.GameScene.Info;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WB.DI;

namespace Scrips.GameScene.View
{


    public class TimeManager : MonoSingleton<TimeManager>
    {
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private Image fillImage;
        [SerializeField]private Timer timer;
        private float TimeLimit;
        public float Time => timer.Current;
        public float RemainTime =>TimeLimit-Time;
        
        private ISceneInfo Scene;

        [Inject]
        private void Construct(ISceneInfo scene)
        {
            Scene = scene;
        }

        // Start is called before the first frame update
        public void GameStart(float limit)
        {
            TimeLimit = limit;
            Scene.GameSet.Subscribe(_ => Stop());
            timer.TimerStart();
        }
        
        private void Update()
        {
            if (!timer.IsRunning) return;
            timerText.text = ((int)Mathf.Ceil(RemainTime)).ToString();
            fillImage.fillAmount = RemainTime / TimeLimit;
            SetTime.I.SetTimeToData(RemainTime);
            if (Mathf.Ceil(RemainTime)<=0)
            {
                new GameSetCommand().Run();
                timer.Stop();
            }
        }

        private void Stop()
        {
            timer.Stop();
        }
    }
}
