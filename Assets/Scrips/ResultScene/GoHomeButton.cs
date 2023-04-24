using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WBTransition;

public class GoHomeButton : MonoBehaviour
{
    private ResultSceneView result;
    private void Start()
    {
        result = FindObjectOfType<ResultSceneView>();
    }

    public void OnClicked()
    {
        SoundManager.I.BGMFade();

        if (result != null)
        {
            SceneFrom from = result.Clear ? SceneFrom.Win : SceneFrom.Lose;
            if (result.AllClear) from = SceneFrom.AllClear;
            SceneManager.LoadScene("Home",null,new Dictionary<string, object>(){{"SceneFrom",from}});
        }
        else
        {
            SceneManager.LoadScene("Home");
        }

        
   
    }
}
