using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WB.Animation;

public class HideTab : MonoBehaviour
{
    [SerializeField] private CanvasGroup cg;

    private bool isShowing;

    private void Start()
    {
        cg.alpha = 0;
        cg.blocksRaycasts = false;
    }

    public void Show()
    {
        isShowing = true;
        cg.blocksRaycasts = true;
        var s = new Sequence()
            .Append(new GeneralAnim(0, 1, x => cg.alpha = x,0.3f));
        s.Play();
    }

    public void Close()
    {
        isShowing = false;
        cg.blocksRaycasts = false;
        var s = new Sequence()
            .Append(new GeneralAnim(1, 0, x => cg.alpha = x,0.3f));
        s.Play();
    }
}
