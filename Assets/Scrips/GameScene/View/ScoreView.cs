using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WB.DI;

namespace Scrips.GameScene.View
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        private IPlayerInfo Player { get; set; }
        [Inject]
        private void Construct(IPlayerInfo player)
        {
            Player = player;
        }
        
        private void Start()
        {
            scoreText.text = "0";
            Player.Score.Subscribe(score=>scoreText.text = score.ToString());
        }
    }
}
