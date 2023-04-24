using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageList : MonoBehaviour
{
    [SerializeField] private GameObject stageItemPrefab;
    [SerializeField] private Transform stageListTrn;
    private StageBook[] stages;

    private UserData userData => SaveSystem.I.UserData;
    
    // Start is called before the first frame update
    void Start()
    {
        
        stages = Resources.LoadAll<StageBook>("Stages");
        
        MakeItem(stages[0],true);
        
        for (int i = 1; i < stages.Length; i++)
        {
            bool available = userData.Cleared_ReadOnly.Contains(i - 1);

                MakeItem(stages[i],available);
            
        }
        
        
        

    }

    private void MakeItem(StageBook stageBook,bool available)
    {
        int highestRecord = userData.GetHighScore(stageBook.StageId);
        
        Instantiate(stageItemPrefab, stageListTrn).GetComponent<StageItem>()
            .Initialize(stageBook,highestRecord,available);
    }
    
    

}
