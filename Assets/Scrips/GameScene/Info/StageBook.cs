using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stage",fileName = "Stage")]
public class StageBook : ScriptableObject
{
    
    public int LimitTime
    {
        get => limitTime;
    }

    public int[] Goals
    {
        get => goals;
    }

    public GameObject[] StagePrefabs
    {
        get => stagePrefabs;
    }


    public int StageId
    {
        get => stageId;
    }
    
    public string StageName => stageName;


    [SerializeField] private int stageId = -1;
    [SerializeField] private string stageName = "Root";
    [SerializeField] private int limitTime;    
    [SerializeField] private int[] goals;
    [SerializeField] private GameObject[] stagePrefabs;
    [SerializeField] private AudioClip bgm;
    [SerializeField] private bool lastStage;

    public bool LastStage => lastStage;

    public AudioClip BGM => bgm;
}
