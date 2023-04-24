using System;
using System.Collections;
using System.Collections.Generic;
using Scrips.Animation;
using Scrips.GameScene.Info;
using Scrips.GameScene.View;
using UnityEngine;
using WB.DI;

public class GuideController : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField]private List<Transform> guidePoints;
    [SerializeField]private List<SpriteRenderer> guideSptires;
    private bool Show => !WBDI.Get<ISceneInfo>().SceneEffects.Contains(SceneEffect.Radar);
    
    public float Vx { get; private set; }
    public float G { get; private set; }

    private void Start()
    {
        timer.TimerStart();
        Align(0);
    }

    // Update is called once per frame
    void Update()
    {
        Align(timer.Current);
    }
    
    
    private void Align(float pTime)
    {
        float vx = 8*Mathf.Sin(Mathf.Min(pTime*3/4f,Mathf.PI/2));
        float g = 8*Mathf.Cos(Mathf.Min(pTime*3/4f,Mathf.PI/2));
        Color guideColor = Show ? Color.white : new Color(0, 0, 0, 0);
        for (int i = 0; i < guidePoints.Count; i++)
        {
            float ii = i / 8f;
            guidePoints[i].position = PlayerView.I.transform.position + BulletView.Trajectory(ii,vx,g);
            guideSptires[i].color = guideColor;
        }

        Vx = vx;
        G = g;
    }
}
