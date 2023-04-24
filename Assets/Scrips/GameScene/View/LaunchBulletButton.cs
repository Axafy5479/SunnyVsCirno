using System.Collections;
using System.Collections.Generic;
using Scrips.GameScene.Info;
using UnityEngine;
using WB.DI;

namespace Scrips.GameScene.View
{
    public class LaunchBulletButton : MonoSingleton<LaunchBulletButton>
    {
        [SerializeField] private GameObject bulletPrefab,guideCtrlPrefab;
private ISceneInfo Scene { get; set; }
        
        [Inject]
        private void Construct(ISceneInfo scene)
        {
            Scene = scene;
        }
        
        public bool IsPressed { get; private set; }
        private GuideController guideCtrl;
        private IAudioSource audioSource;
        private GuideController GuideCtrl
        {
            get => guideCtrl;
            set
            {
                if (guideCtrl != null) Destroy(guideCtrl.gameObject);
                guideCtrl = value;
            }
        }

        private void Start()
        {
            Scene.GameSet.Subscribe(_ =>
            {
                if (IsPressed) OnPointerUp();
            });
        }
        

        public void OnPointerDown()
        {
            if (!StageView.I.IsRunning.Value || GuideCtrl != null || IsPressed) return;
            
            GuideCtrl = Instantiate(guideCtrlPrefab).GetComponent<GuideController>();
            IsPressed = true;
            audioSource = SEPlayerAssist.I.Play(SEType.CHARGE);
        }

        public void OnPointerUp()
        {
            audioSource?.Stop();
            if (StageView.I.IsRunning.Value && GuideCtrl != null && IsPressed)
            {
                SEPlayerAssist.I.Play(SEType.SHOOT);
                Shoot(GuideCtrl.Vx, GuideCtrl.G);
            }

            IsPressed = false;
            if (GuideCtrl != null)
            {
                Destroy(GuideCtrl.gameObject);
                GuideCtrl = null;
            }
        }

        private void Shoot(float Vx,float g)
        {
            GameObject bulletObj = Instantiate(bulletPrefab);
            bulletObj.transform.position = PlayerView.I.transform.position;
            bulletObj.GetComponent<BulletView>().Shoot(Vx,g);
        }
    }
}
