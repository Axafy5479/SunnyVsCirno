using System;
using System.Collections;
using System.Collections.Generic;
using DependencyInjection;
using RX;
using Scrips.GameScene.Info;
using UnityEngine;

namespace Scrips.GameScene.Data
{
    public class Enemy:IEnemyInfo
    {
        public Enemy(EnemyBook book)
        {
            Book = book;
            _hp = new Subject<int>(book.Hp);
        }
        
        public EnemyBook Book { get; }
        
        private Subject<int> _hp { get; set; }
        public RX.IObservable<int> Hp => _hp;

        public bool Crash()
        {
            _hp.OnNext(Hp.Value-1);
            if (Hp.Value <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
    }
}
