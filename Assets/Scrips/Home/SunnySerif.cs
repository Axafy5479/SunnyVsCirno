using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using WB.Animation;
using Random = UnityEngine.Random;

public enum Expression
{
    Normal,Happy,Surprised,Shy,Introuble,BitterSmile,Irritated
}
public class SunnySerif : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private ExpressionSet[] expressionSunnyTexture;
    [SerializeField] private TextMeshProUGUI serifText;
    private Transform sunnyTrn;
    [SerializeField] private Image sunnyImage;
    private Dictionary<Expression, Sprite> exp_texture_map = new Dictionary<Expression, Sprite>();
    [SerializeField] private GameObject serifBaloon;
    private Sequence sunny_talking;
    private void Start()
    {
        serifBaloon.SetActive(false);
        sunnyTrn = sunnyImage.transform;
        foreach (var pair in expressionSunnyTexture)
        {
            exp_texture_map.Add(pair.Expression,pair.SunnyImage);
        }
    }

    public void Talk(SerifSetting serifSetting)
    {
        if (sunny_talking == null)
        {
            Show(serifSetting);
        }
    }

    private readonly List<SerifSetting> serifs = new List<SerifSetting>()
    {
        new SerifSetting("プレイしてくれてありがとう!\n私、頑張るよ",Expression.Happy,3), 
        new SerifSetting("実は私って大の字のポーズを取ったら\nフリルが円形に並ぶんだよ!\nほら! ほら!",Expression.Happy,4),
        new SerifSetting("私の能力は最強よ!\n透明人間にもなれるけど\n顕微鏡、望遠鏡だってこの通り!",Expression.Happy,4),
        new SerifSetting("私の能力は最強なんだけど\nやっぱりルナとスターがいないと\n完ぺきにはならないんだよね",Expression.Normal,4),
        new SerifSetting("ねぇ聞いて聞いて!\n私目撃しちゃった!!\nなんとルナったら私達に隠れt ... ",Expression.Happy,4),
        new SerifSetting("このまえ太陽の光を一点に集めようとしたんだけど\n魔力不足で出来なかった...\n魔理沙によるとめっちゃ魔力が必要になるんだって!",Expression.Normal,5),
        new SerifSetting("ねぇねぇ、さっき湖の周りを散歩してたら\nチルノの9点のテストが落ちてたの!\nほんと馬鹿だよね～(笑)",Expression.Happy,4),
        new SerifSetting("今日の夢なんだけど\nチルノをぼっこぼこにして踏みつける夢だったの!\nあ～楽しかったっ♪",Expression.Shy,4),
        new SerifSetting("たまになんだけど、にとりの実験に呼ばれることがあるんだ\n光を使う実験の場合、私の能力は凄いらしい!\n私って理系女子かも!",Expression.Happy,5),
        new SerifSetting("アリスって知ってる? 人形遣いの\n魔理沙の家の中を見せてってお願いされてるんだけど\n盗撮?だよね、どうしよう...\nあめちゃんくれるらしいんだよね",Expression.Introuble,5),
        new SerifSetting("スターって凄いんだ～!\n私は能力使うぞって意識しないと発動しないんだけど\nスターは無意識でも察知できるんだ!\nおかげで不意打ちはくらわないの",Expression.Happy,5),
        new SerifSetting("ルナって凄いんだ!\n忍び込むときなんかルナがいないと絶対にばれちゃうし\n隠れるときも音を消さないとダメなのよ",Expression.Happy,4),
        new SerifSetting("今日のいたずらは完ぺきだったよ!\n魔理沙をこっそり透明人間にするいたずら!\nみんなに無視されて泣きそうだったのよwww",Expression.Happy,5),
        new SerifSetting("コーヒーってどう思う??\nルナ、なんであんなに苦い飲物が好きなのかしら。\nオレンジジュースのほうが美味しくない?",Expression.BitterSmile,5),
        new SerifSetting("どうやったら建物の中でも透明になれるのかな...\nたくさんライトがあるから\n操作しないといけない光が多すぎるっ!\n影も消さないとだし...",Expression.Introuble,5),
        new SerifSetting("え、私??\nいやいやルナとかスターのほうがかわいいよ\n絶対",Expression.BitterSmile,3),
        new SerifSetting("幽香は怖いって噂だけど\n全然そんなことないんだ!\nどうしてかな?\n妖精には優しいの?",Expression.BitterSmile,4),
        new SerifSetting("私、ひまわりが大好きなんだ!\n綺麗だしかっこいいし大きいし\nあと、英語ではサニーフラワーって言うらしいし",Expression.Happy,4),
        new SerifSetting("ねぇどうしたら早起きできるの?\n日の光の妖精なのに11時に起きたりするの\nちょっとだけ恥ずかしい...\nスターに「おそようw」とか言われるし",Expression.BitterSmile,5),
        new SerifSetting("闇を操る～とか千里眼～とか言ってる妖怪がいるけど\n私にだってできるのよ!\nほら、こっからでも博麗神社が見える\n霊夢が空の賽銭箱見てしょんぼりしてる(笑)",Expression.Happy,5),
        new SerifSetting("いたずらといっても、痛いことはしちゃだめなんだ\n「クソガキめ～!」って顔が見られないからね",Expression.Happy,4),
        new SerifSetting("私たち妖精は毎日が日曜日!\nいつも好きなことをして過ごすの\n羨ましいでしょ",Expression.Happy,3),
        new SerifSetting("うーん、どうやったら魔理沙みたいに強くなれるのかな...\n魔力が足りないのかな?\n戦略が悪いのかな?\n私たちの能力は強いはずなんだけど",Expression.Introuble,5),
        new SerifSetting("ねぇ、料理の仕方教えて!\n私だけ何も作れないんだけど、やばいよね",Expression.BitterSmile,3),
        new SerifSetting("ミルクちゃんって呼んだら\n顔面ダイレクトサンライトだから!",Expression.Irritated,3),
        new SerifSetting("むしゃくしゃするなぁ\nこんな時はチルノをボコるに限るんだけど",Expression.Irritated,3),
        new SerifSetting("ルナったらしょっちゅう本読んでるんだけど\n何が面白いんだろう\n5文以上の文章はお断りよね～",Expression.Introuble,4),
        new SerifSetting("私たちは3人揃った時が1番強いんだけど\nスターったらしょっちゅう勝手に消えるんだよ!\n次やったらお仕置きしたいんだけど\n何かいい案ない?",Expression.Irritated,3),
        new SerifSetting("人気投票で私たちに投票しないと\n許さないわよ!",Expression.Irritated,3),
        new SerifSetting("ん? あなたも千里眼を体験したいの?\nいいよ!こっちにきて\nどこら辺を見てみたい??",Expression.Happy,4),
        new SerifSetting("三人で力を合わせても\nたまに気付かれることがあるのよ!\nなんでなんだろう...\n視覚、聴覚、、、\nまさか嗅覚!?",Expression.Surprised,5)
        
    };

    public void OnPointerClick(PointerEventData eventData)
    {
        if (sunny_talking==null)
        {
            var serif = serifs[Random.Range(0, serifs.Count)];
            Show(serif);
        }
    }

    private void Show(SerifSetting serif)
    {
        serifBaloon.SetActive(true);
        serifText.text = serif.Serif;
        sunnyImage.sprite = exp_texture_map[serif.Expression];

        sunny_talking = new Sequence()
            .Append(new GeneralAnim(1, 0.96f, scale => sunnyTrn.localScale = new Vector2(scale, scale), 0.1f))
            .Append(new GeneralAnim(0.96f, 1, scale => sunnyTrn.localScale = new Vector2(scale, scale), 0.1f))
            .Append(new Delay(serif.ShowingTime))
            .Append(new CallbackMethod(() =>
            {
                sunnyImage.sprite = exp_texture_map[Expression.Normal];
                serifBaloon.SetActive(false);
                sunny_talking = null;
            }));
        
        sunny_talking.Play();

    }

    private void OnDestroy()
    {
        sunny_talking?.Kill();
    }
}

public struct SerifSetting
{
    public SerifSetting(string serif, Expression expression, int showingTime)
    {
        Serif = serif;
        Expression = expression;
        ShowingTime = showingTime;
    }

    public string Serif { get; }
    public Expression Expression { get; }
    public int ShowingTime{get;}
}

[Serializable]
public class ExpressionSet
{
    public Expression Expression => expression;
    public Sprite SunnyImage => sunnyImage;

    [SerializeField] private Expression expression;
    [SerializeField] private Sprite sunnyImage;
}