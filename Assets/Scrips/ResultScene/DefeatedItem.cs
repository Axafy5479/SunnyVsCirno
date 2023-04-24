using System.Collections;
using System.Collections.Generic;
using Scrips.GameScene.Info;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scrips.ResultScene
{
    public class DefeatedItem : MonoBehaviour
    {
        [SerializeField] private Image iconTexture;
        [SerializeField] private TextMeshProUGUI numberText;
        public void Initialize(EnemyBook book, int number)
        {
            iconTexture.sprite = book.Texture;
            numberText.text = number.ToString();
        }
    }
}
