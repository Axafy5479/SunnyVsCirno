using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WB.Animation;

public class Star : EnemyView
{
    [SerializeField] private SpriteRenderer iconTexture;
    private Sequence s;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        s = new Sequence()
            .Append(new Delay(2))
            .Append(new GeneralAnim(1, 0, x =>
            {
                Color c = iconTexture.color;
                iconTexture.color = new Color(c.r, c.g, c.b, x);
            },1))
            .Append(new CallbackMethod(()=>
            {
                iconTexture.gameObject.SetActive(false);
                s = null;
            }));
        s.Play();
    }

    private void OnDestroy()
    {
        s?.Kill();
    }
}
