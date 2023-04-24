using Scrips.Animation;
using Scrips.GameScene.Command;
using Scrips.GameScene.Info;
using Scrips.GameScene.View;
using UnityEngine;
using WB.DI;

public class BulletView : MonoBehaviour
{
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private Timer timer;
    [SerializeField] private float bulletSpeedRatio = 3;
    public IBulletInfo BulletInfo { get; set; }
    private Transform Trn { get; set; }
    
    private float Vx { get; set; }
    private float G { get; set; }

    private void Start()
    {
        BulletInfo.AddPoint.Subscribe(killResult=>
        {
            int point = killResult.Item2;
            EnemyBook enemy = killResult.Item1;
            SoundManager.I.PlaySE(enemy.KillSe);
            Transform pointTrn = Instantiate(pointPrefab).transform;
            pointTrn.position = Trn.position;
            pointTrn.Translate(new Vector3(0.5f,0,0));

            Color textColor = enemy.CharaColor;
            if(WBDI.Get<ISceneInfo>().SceneEffects.Contains(SceneEffect.LunaticTime))
            {
                textColor = Color.red;
            }
            pointTrn.GetComponent<ObtainedPointShower>().StartMoving(point,textColor);
        });
    }

    private Vector3 InitialPos { get; set; }

    public void Shoot(float vx,float g)
    {
        Trn = this.transform;
        Vx = vx;
        G = g;
        InitialPos = PlayerView.I.transform.position;
        var bulletCommand = new ShootCommand();
        bulletCommand.Run();
        BulletInfo = bulletCommand.Bullet;
        timer.TimerStart();
    }
    

    private void Update()
    {
        Trn.position = InitialPos + Trajectory(timer.Current*bulletSpeedRatio,Vx,G);
    }

    public static Vector3 Trajectory(float t,float Vx,float g)
    {
        return new Vector3(Vx*t, -(g*t*t)/2, 0);
    }
    
}
