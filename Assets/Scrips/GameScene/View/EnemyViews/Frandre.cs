using System.Collections;
using System.Collections.Generic;
using Scrips.GameScene.Command;
using Scrips.GameScene.Info;
using Scrips.GameScene.View;
using UnityEngine;

public class Frandre : EnemyView
{
    [SerializeField] private GameObject aosuji;
    private Coroutine coroutine;
    protected override void Start()
    {
        base.Start();
        aosuji.SetActive(false);
    }

    protected override bool Crash(IBulletInfo bullet)
    {
        if (coroutine == null)
        {
            coroutine = StartCoroutine(Angry());
        }

        return false;
    }

    private IEnumerator Angry()
    {
        SEPlayerAssist.I.Play(SEType.POKA);
        yield return new WaitForSeconds(1f);
        aosuji.SetActive(true);
        SEPlayerAssist.I.Play(SEType.ANGRY);
        yield return new WaitForSeconds(1f);
        bool broke = false;
        foreach (Transform trn in StageView.I.transform.GetChild(0))
        {
            foreach (Block childBlock in trn.GetComponentsInChildren<Block>())
            {
                if (childBlock is Block block)
                {
                    block.Brake();
                    broke = true;
                }
            }

        }

        if (broke)
        {
            SEPlayerAssist.I.Play(SEType.BlockBrake);
        }
        new RunAwayCommand(EnemyInfo).Run();
        Destroy(this.gameObject);
        
    }
}
