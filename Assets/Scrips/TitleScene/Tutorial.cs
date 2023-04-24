using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Sprite[] textures;
    [SerializeField] private Image image;
    [SerializeField] private CanvasGroup cg;
    [SerializeField] private CanvasGroup nextButtonCG;
    [SerializeField] private CanvasGroup prevButtonCG;

    private int currentPage = 0;
    private void Start()
    {
        Hide();
    }

    public void Show()
    {
        cg.alpha = 1;
        cg.blocksRaycasts = true;

        image.sprite = textures[0];

        CheckButtonEnabled();
    }

    public void OnNextButtonClicked()
    {
        if (currentPage < textures.Length - 1)
        {
            currentPage++;
            image.sprite = textures[currentPage];
        }
        CheckButtonEnabled();
    }

    public void OnCloseButtonClicked()
    {
        Hide();
    }
    
    public void OnPrevButtonClicked()
    {
        if (currentPage > 0)
        {
            currentPage--;
            image.sprite = textures[currentPage];
        }

        CheckButtonEnabled();
    }

    private void Hide()
    {
        cg.alpha = 0;
        cg.blocksRaycasts = false;
    }

    private void CheckButtonEnabled()
    {
        bool next = currentPage < textures.Length - 1;
        bool prev = currentPage > 0;

        nextButtonCG.alpha = next ? 1 : 0.5f;
        nextButtonCG.blocksRaycasts = next;
        
        prevButtonCG.alpha = prev ? 1 : 0.5f;
        prevButtonCG.blocksRaycasts = prev;
    }
}
