using System;
using System.Collections;
using System.Collections.Generic;
using Scrips.GameScene.Command;
using Scrips.GameScene.Info;
using TMPro;
using UnityEngine;
using WB.DI;

public class GreatFairy : EnemyView
{
    [SerializeField] private int limit;
    [SerializeField] private TextMeshPro remainTimeText;
    private Coroutine coroutine;
    private IDisposable _disposable;
    protected override void Start()
    {
        base.Start();
        coroutine = StartCoroutine(CountDown());
        remainTimeText.text = "";
        _disposable = WBDI.Get<ISceneInfo>().GameSet.Subscribe(_ =>
        {
            StopCoroutine(coroutine);
        });
    }
    
    

    private IEnumerator CountDown()
    {
        int remain = limit;

        while (remain>0)
        {
            yield return new WaitForSeconds(1);
            remain--;
            
            if (remain < 6)
            {
                remainTimeText.text = remain.ToString();
            }
        }
        
        Destroy(this.gameObject);
        new RunAwayCommand(EnemyInfo).Run();
    }

    private void OnDestroy()
    {
        _disposable.Dispose();
    }
}
