using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowKilledEnemy : MonoBehaviour
{
    [SerializeField] private GameObject prevArrow;
    [SerializeField] private GameObject nextArrow;

    [SerializeField] private Image charaImage;
    [SerializeField] private TextMeshProUGUI charaName;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI killedNum;
    [SerializeField] private TextMeshProUGUI killedText;

    private int CurrentPage = 0;
    private List<EnemyBookData> KilledEnemyData { get; set; }
    public void Initialize(List<EnemyBookData> killedEnemyData)
    {
        KilledEnemyData = killedEnemyData;
    }
    public void Show(int page)
    {
        CurrentPage = page;
        bool prev = page > 0;
        bool next = page < KilledEnemyData.Count - 1;

        prevArrow.SetActive(prev);
        nextArrow.SetActive(next);

        if (KilledEnemyData.Count == 0)
        {
            charaImage.color = new Color(0,0,0,0);
            killedText.color = new Color(0,0,0,0);
            killedNum.color = new Color(0,0,0,0);
            return;
        }
        
        EnemyBookData showingEnemy = KilledEnemyData[page];
        charaImage.sprite = showingEnemy.Book.Texture;
        charaImage.color = Color.white;
        killedText.color = Color.black;
        charaName.text = showingEnemy.Book.EnemyName;
        description.text = showingEnemy.Book.Description;
        killedNum.text = showingEnemy.KillNum.ToString();
        killedNum.color = Color.black;
    }

    public void OnNextButtonClicked()
    {
        CurrentPage++;
        Show(CurrentPage);
    }
    public void OnPrevButtonClicked()
    {
        CurrentPage--;
        Show(CurrentPage);
    }
}
