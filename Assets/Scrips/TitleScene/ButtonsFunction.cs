using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WBTransition;

public class ButtonsFunction : MonoBehaviour
{
    [SerializeField] private Scene homeScene;
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene(homeScene,null,new Dictionary<string, object>(){{"SceneFrom",SceneFrom.Title}});
    }

    public void OnExplainButtonClicked()
    {
        
    }
}
