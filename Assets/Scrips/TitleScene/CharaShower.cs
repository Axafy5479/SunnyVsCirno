using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using WB.Animation;

public class CharaShower : MonoBehaviour
{
    [SerializeField] private Transform sunny,cirno,titleText;
    [SerializeField] private CanvasGroup startButton,explainButton;

    private float showingTime = 0.3f;
    private float goalY = -120;
    
    // Start is called before the first frame update
    void Start()
    {
        var s = new Sequence()
            .Append(new Delay(0.3f))
            .Append(new MoveAnimY(cirno, goalY, 0.3f, true))
            .Append(new Delay(0.3f))
            .Append(new MoveAnimY(sunny, goalY, 0.3f, true))
            .Append(new Delay(0.3f))
            .Append(new MoveAnimY(titleText, 178.24f, 0.3f, true))
            .Join(new GeneralAnim(0, 1, x =>
            {
                startButton.alpha = x;
                explainButton.alpha = x;
            }, 0.3f))
            .Append(new CallbackMethod(() =>
            {
                startButton.blocksRaycasts = true;
                explainButton.blocksRaycasts = true;
            }));
        s.Play();
    }
}
