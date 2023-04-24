using System;
using System.Collections;
using System.Collections.Generic;
using Scrips.GameScene.Command;
using Scrips.GameScene.Info;
using UnityEngine;

public abstract class EnemyView : MonoBehaviour
{
    [SerializeField] private EnemyBook book;
    public EnemyBook Book => book;
    public IEnemyInfo EnemyInfo { get; set; }

    protected virtual void Start()
    {
        EnemyInfo.Hp.Subscribe(hp =>
        {
            if (hp == 0)
            {
                Destroy(this.gameObject);
            }
        });
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject is GameObject go)
        {
            if (go.GetComponent<BulletView>() is BulletView bullet)
            {
                if (!Crash(bullet.BulletInfo))
                {
                    Destroy(go);
                }
            }
        }
    }

    protected virtual bool Crash(IBulletInfo bullet)
    {
        var command = new DefeatEnemyCommand(bullet, EnemyInfo);
        command.Run();
        return command.Killed;
    }

}
