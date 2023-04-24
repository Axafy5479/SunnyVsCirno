using System;
using System.Collections;
using System.Collections.Generic;
using DependencyInjection;
using Scrips.GameScene.Info;
using UnityEngine;
using WB.DI;

namespace Scrips.ResultScene
{
    public class DefeatedEnemyFrame : MonoBehaviour
    {
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private Transform frameTrn;
        
        private Dictionary<EnemyBook, int> resultDic = new Dictionary<EnemyBook, int>();
        
        private IPlayerInfo Player { get; set; }

        [Inject]
        private void Construct(IPlayerInfo player)
        {
            Player = player;
        }
        
        private void Start()
        {
            foreach (var enemy in Player.DefeatedEnemies)
            {
                if (resultDic.ContainsKey(enemy.Book))
                {
                    resultDic[enemy.Book]++;
                }
                else
                {
                    resultDic.Add(enemy.Book,1);
                }
            }

            foreach (var e in resultDic)
            {
                DefeatedItem enemyItem = Instantiate(itemPrefab, frameTrn).GetComponent<DefeatedItem>();
                enemyItem.Initialize(e.Key,e.Value);
            }
        }
    }
}
