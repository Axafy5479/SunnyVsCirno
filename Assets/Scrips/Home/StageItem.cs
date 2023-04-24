using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WBTransition;

public class StageItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleNameText;
    [SerializeField] private TextMeshProUGUI[] goalTexts;
    [SerializeField] private TextMeshProUGUI RecordText;
    [SerializeField] private GameObject lockIcon;
    [SerializeField] private Image gotStarImage;
    [SerializeField] private Sprite[] starTextures;
    private StageBook StageBook { get; set; }

    public void Initialize(StageBook book,int highestRecord,bool available)
    {
        StageBook = book;
        titleNameText.text = book.StageName;
        for (int i = 0; i < 3; i++)
        {
            goalTexts[i].text = book.Goals[i].ToString();
        }
        RecordText.text = highestRecord.ToString();
        lockIcon.SetActive(!available);
        this.GetComponent<Button>().enabled = available;

        gotStarImage.color = new Color(0,0,0,0);
        for (int i = 0; i < 3; i++)
        {
            if (highestRecord >= book.Goals[i])
            {
                gotStarImage.color = Color.white;
                gotStarImage.sprite = starTextures[i];
            }
        }
    }

    public void OnClicked()
    {
        var sendingData = new Dictionary<string, object>()
        {
            {"stageBook", StageBook},
        
        };
        var sendingDataAfter = new Dictionary<string, object>()
        {
            {"bgmContinue",false}
        };
        
        SoundManager.I.BGMFade();
        SceneManager.LoadScene("GameScene",sendingData,sendingDataAfter);
    }
    
}
