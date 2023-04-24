using System.Collections;
using System.Collections.Generic;
using Scrips.GameScene.Info;
using UnityEngine;

public struct EnemyBookData
{
    public EnemyBookData(EnemyBook book, int killNum)
    {
        Book = book;
        KillNum = killNum;
    }

    public EnemyBook Book { get; }
    public int KillNum { get; }
}
