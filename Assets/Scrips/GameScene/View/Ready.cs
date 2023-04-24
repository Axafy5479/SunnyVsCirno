using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WB.Animation;

public class Ready : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI readyText;

    public void Show(List<(string text,float showingTime)> textData,Action onEndShowing)
    {
        var s = new Sequence();
        foreach (var t in textData)
        {
            s.Append(new CallbackMethod(() =>
                {
                    SEPlayerAssist.I.Play(SEType.WADAIKO);
                    readyText.text = t.text;
                }))
                .Append(new Delay(t.showingTime));
        }

        s.Append(new CallbackMethod(() =>
        {
            onEndShowing();
            Destroy(this.gameObject);
        }));
        
        s.Play();
    }
}
