using System;
using System.Collections;
using System.Collections.Generic;
using DependencyInjection;
using Scrips.GameScene.Info;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using WB.DI;

public class ResultSceneView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI satisfyText;
    [SerializeField] private TextMeshProUGUI timeBonusText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI[] goalTexts;
    [SerializeField] private GameObject sunny_win;
    [SerializeField] private GameObject sunny_lose;
    [SerializeField] private TMP_FontAsset noto;
    [SerializeField] private Sprite result_bg_cloud;
    [SerializeField] private Image bgImage;
    [SerializeField] private GameObject newRecordObj;
    [SerializeField] private TextMeshProUGUI winSerifText;
    [SerializeField] private TextMeshProUGUI loseSerifText;
    [SerializeField] private RetryButton retryButton;

    public bool AllClear { get; private set; }
    
    public bool Clear { get; private set; }
    private IPlayerInfo Player { get; set; }
    private ISceneInfo Scene { get; set; }

    
    private List<string> winSerif = new List<string>()
    {
        "チルノちゃ～ん\nごめんなさい は?",
        "妖精最強は\n間違いなくこの私!",
        "今日の作戦も\n大成功!!",
        "あれ? \n誰がサイキョーだって?",
        "やったー!\nルナとスターにも\n報告しなきゃ♪",
        "私に勝てない相手など\nいないのだ!"
    };

    [Inject]
    private void Construct(IPlayerInfo player,ISceneInfo scene)
    {
        Player = player;
        Scene = scene;
    }
    
    private void Start()
    {

        int score = Player.Score.Value;
        int timeBonus = (int)(Scene.RemainTime*10);
        int finalScore = score+timeBonus;
        int minGoal = Scene.StageBook.Goals[0];
        for (int i = 0; i < 3; i++)
        {
            goalTexts[i].text = Scene.StageBook.Goals[i].ToString();
        }

        bool cleared = SaveSystem.I.UserData.Cleared_ReadOnly.Contains(Scene.StageBook.StageId);
        var clear_newRecord = SaveSystem.I.ChangeData(Scene.StageBook,Player.DefeatedEnemies,finalScore);
        Clear = Array.Exists(clear_newRecord.cleared,c=>c);
        
        if (Scene.StageBook.LastStage)
        {
            
            if (!cleared && Clear)
            {
                AllClear = true;
                retryButton.Hide();
            }
        }

        
        sunny_win.SetActive(Clear);
        sunny_lose.SetActive(!Clear);

        if (Clear)
        {
            if (Setting.RUN_ON_ATUMARU && clear_newRecord.newRecod)
            {
                Atsumaru.Comment.ChangeScene("Result"+Scene.StageBook.StageId);
                Atsumaru.scoreboards.setRecord((Scene.StageBook.StageId+1).ToString(), finalScore.ToString());
            }
            
            string serif = winSerif[UnityEngine.Random.Range(0, winSerif.Count)];
            winSerifText.text = serif;
        }
        
        
        newRecordObj.SetActive(clear_newRecord.newRecod);
        
        timeBonusText.text = timeBonus.ToString();
        satisfyText.text = score.ToString();
        scoreText.text = finalScore.ToString();
        if (!Clear)
        {
            scoreText.font = noto;
            bgImage.sprite = result_bg_cloud;
        }

        //goalText.text = goal.ToString();

    }
}
