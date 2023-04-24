using TMPro;
using UnityEngine;
using WB.Animation;

public class ObtainedPointShower : MonoBehaviour
{ 
    [SerializeField] private TextMeshPro pointText;
    [SerializeField] private float moveDis = 1f;
    [SerializeField] private float moveT = 0.6f;
    private Transform Trn { get; set; }
    
    public void StartMoving(int point,Color charaColor)
    {
        pointText.color = charaColor;
        pointText.text = point.ToString();
        Trn = this.transform;

        var s = new Sequence()
            .Append(new MoveAnimY(Trn, Trn.localPosition.y + moveDis, moveT, true).SetEase(Ease.OutSine))
            .Join(new GeneralAnim(1, 0, x =>
            {
                var prevC = charaColor;
                pointText.color = new Color(prevC.r, prevC.g, prevC.b, x);
            }, moveT).SetEase(Ease.InSine))
            .Append(new CallbackMethod(()=>Destroy(this.gameObject)));
        s.Play();
    }
}
