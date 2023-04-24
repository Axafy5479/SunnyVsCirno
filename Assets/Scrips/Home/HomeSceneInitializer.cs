using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using WBTransition;

public enum SceneFrom
{
    Title,Win,Lose,AllClear
}
public class HomeSceneInitializer : MonoBehaviour,ISceneInitializer
{
    [SerializeField] private AudioClip bgm;
    [SerializeField] private SunnySerif sunny;
    void Start()
    {
        if (Setting.RUN_ON_ATUMARU)
        {
            Atsumaru.Comment.ChangeScene("Home");
        }

        SoundManager.I.PlayBGM(bgm);
    }

    public IEnumerator BeforeOpenMask(Dictionary<string, object> args)
    {
        yield return null;
    }

    public void AfterOpenMask(Dictionary<string, object> args)
    {
        if (args!=null&&args.ContainsKey("SceneFrom"))
        {
            var from = (SceneFrom) args["SceneFrom"];
            switch (from)
            {
                case SceneFrom.Title:
                    sunny.Talk(new SerifSetting("プレイしてくれてありがとう!\n全画面にしないと\n見切れちゃうかもだから注意です!",Expression.Happy,5));
                    break;
                case SceneFrom.Lose:
                    sunny.Talk(new SerifSetting("今のは、、、全然本気じゃないし!\n本当は余裕で勝てたところよ\nほ、ほんとなんだから!",Expression.Introuble,4));
                    break;
                case SceneFrom.Win:
                    sunny.Talk(new SerifSetting("あ～楽しかった♪\nこれぞ光の三妖精のリーダーって感じ\n次よ!次!!!",Expression.Happy,4));
                    break;
                case SceneFrom.AllClear:
                    sunny.Talk(new SerifSetting("え、完全クリア!?\nおめでとう!!\n最後までプレイしてくれてありがとう!!!\n三妖精メンバーに入る権利をあげるわっ",Expression.Happy,7));
                    break;
                default:
                    throw new Exception("SceneFrom:" + from + "のセリフは登録されていません");
            }
        }
    }
}
