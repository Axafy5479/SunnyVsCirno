using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WB.Animation;

public abstract class HomeTabBase : MonoBehaviour
{
    [SerializeField] protected HideTab hideTab;
    protected abstract float HideY { get; }
    protected abstract float ShowY{get;}
    private bool isShowing;

    private void Move(bool show)
    {
        isShowing = show;
        float goal = show ? ShowY : HideY;
        var s = new Sequence().Append(new MoveAnimY(this.transform, goal, 0.3f, true));
        s.Play();
    
        if (show)
        {
            this.transform.SetAsLastSibling();
            hideTab.Show();
            OnShow();
        }
        else
        {
            this.transform.SetAsFirstSibling();
            hideTab.Close();
            OnHide();
        }
        
    }
    
    public void OnClicked()
    {
        Move(!isShowing);
    }

    protected abstract void OnShow();
    protected abstract void OnHide();
}
   