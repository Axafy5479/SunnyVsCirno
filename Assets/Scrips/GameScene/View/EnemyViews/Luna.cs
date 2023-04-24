using System;
using System.Collections;
using System.Collections.Generic;
using Scrips.GameScene.Info;
using UnityEngine;

public class Luna : EnemyView
{
    [SerializeField] private GameObject hp2Texture;
    protected override void Start()
    {
        base.Start();
        EnemyInfo.Hp.Subscribe(hp =>
        {
            switch (hp)
            {
                case 0:
                    break;
                case 1:
                    Destroy(hp2Texture);
                    break;
                case 2:
                    hp2Texture.SetActive(true);
                    break;
                default:
                    throw new NotImplementedException();
            }
        });


    }


}
