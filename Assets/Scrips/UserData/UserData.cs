using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class UserData
{


    [SerializeField]
    private List<IntIntPair> killedEnemies = new List<IntIntPair>();
    [SerializeField]
    private List<IntIntPair> stageRecords = new List<IntIntPair>();
    [SerializeField]
    private List<int> clearedStage = new List<int>();
    [SerializeField] 
    private float bgmVolume = 0.3f;
    [SerializeField] 
    private float seVolume =0.3f;
    public Dictionary<int,int> Enemies_ReadOnly => killedEnemies.ToDictionary(l=>l.Id,l=>l.Value);
    public Dictionary<int,int> Records_ReadOnly => stageRecords.ToDictionary(l=>l.Id,l=>l.Value);
    public HashSet<int> Cleared_ReadOnly => new HashSet<int>(clearedStage);
    public float BGMVolume => bgmVolume;
    public float SeVolume => seVolume;
    public int GetHighScore(int stageId)
    {
        return Records_ReadOnly.ContainsKey(stageId) ? Records_ReadOnly[stageId] : 0;
    }
    
    internal void ChangeRecord(Dictionary<int,int> Enemies,Dictionary<int,int> Records,HashSet<int> Cleared)
    {
        killedEnemies = new List<IntIntPair>();
        foreach (var e in Enemies)
        {
            killedEnemies.Add(new IntIntPair(e.Key,e.Value));
        }

        stageRecords = new List<IntIntPair>();
        foreach (var r in Records)
        {
            stageRecords.Add(new IntIntPair(r.Key,r.Value));
        }

        clearedStage = new List<int>(Cleared);
    }

    internal void SetVolume(float bgm, float se)
    {
        bgmVolume = bgm;
        seVolume = se;
    }
    

}

[Serializable]
internal class IntIntPair
{
    public IntIntPair(int id, int initValue)
    {
        this.id = id;
        val = initValue;
    }

    [SerializeField] private int id;
    [SerializeField] private int val;


    public int Id => id;
    public int Value
    {
        get => val;
        set => val = value;
    }
}