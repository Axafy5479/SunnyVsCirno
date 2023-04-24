using System;
using System.Collections;
using System.Collections.Generic;
using Scrips.GameScene.Info;
using UnityEngine;

public class EnemyBookTab : HomeTabBase
{
    [SerializeField] private ShowKilledEnemy killEnemyShower;
    protected override float HideY => 440.2f;
    protected override float ShowY { get; }
    protected override void OnShow()
    {
        var killedEnemyId_Num_Dic = SaveSystem.I.UserData.Enemies_ReadOnly;
        var allEnemy = Resources.LoadAll<EnemyBook>("Characters");

        List<EnemyBookData> killEnemyData = new List<EnemyBookData>();
        foreach (var killedEnemy in killedEnemyId_Num_Dic)
        {
            EnemyBook enemyBook = Array.Find(allEnemy, enemy => enemy.EnemyId == killedEnemy.Key);
            killEnemyData.Add(new EnemyBookData(enemyBook,killedEnemy.Value));
        }
        
        killEnemyShower.Initialize(killEnemyData);
        killEnemyShower.Show(0);
    }

    protected override void OnHide()
    {
        //何もしなくてOK
    }
}
