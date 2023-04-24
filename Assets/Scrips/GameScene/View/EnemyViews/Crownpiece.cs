using System.Collections;
using System.Collections.Generic;
using Scrips.GameScene.Command;
using Scrips.GameScene.Info;
using Scrips.GameScene.View;
using UnityEngine;

public class Crownpiece : EnemyView
{
    [SerializeField] private GameObject lunaticText;
    private Coroutine coroutine;
    protected override void Start()
    {
        base.Start();
        lunaticText.SetActive(false);
    }

    protected override bool Crash(IBulletInfo bullet)
    {
        if (coroutine == null)
        {
            coroutine = StartCoroutine(StartLuneticTime());
        }

        return false;
    }

    private IEnumerator StartLuneticTime()
    {
        SEPlayerAssist.I.Play(SEType.POKA);
        yield return new WaitForSeconds(1f);
        lunaticText.SetActive(true);
        new AddEffectCommand(SceneEffect.LunaticTime).Run();
        //SEPlayerAssist.I.Play(SEType.ANGRY);
        yield return new WaitForSeconds(1f);

        new RunAwayCommand(EnemyInfo).Run();
        Destroy(this.gameObject);
        
    }
}
