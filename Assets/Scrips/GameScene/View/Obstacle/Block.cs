using System;
using System.Collections;
using System.Collections.Generic;
using Scrips.GameScene.Command;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private GameObject particle;
    [SerializeField] private bool destroyEnemy = false;
    public void Brake()
    {
        Instantiate(particle).transform.position = this.transform.position;
        Destroy(this.gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject is GameObject go)
        {
            if (go.GetComponent<BulletView>() is BulletView bullet)
            {
                Destroy(bullet.gameObject);
            }
            
            if (destroyEnemy && go.GetComponent<EnemyView>() is EnemyView enemy)
            {
                new RunAwayCommand(enemy.EnemyInfo).Run();
                Destroy(enemy.gameObject);
            }
        }
    }
}
