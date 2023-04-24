using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Scrips.GameScene.Info;
using UnityEngine;

public class SaveSystem
{
    internal const string PREFS_KEY = "UserData";
    
    #region Singleton

    private static SaveSystem instance;
    public static SaveSystem I => instance ??= new SaveSystem();

    private SaveSystem()
    {
        Load();
    }



    #endregion

    public UserData UserData { get; private set; }

    public void SetVolume(float bgm,float se)
    {
        UserData.SetVolume(bgm, se);
        Save();
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="stageBook"></param>
    /// <param name="newlyKilledEnemies"></param>
    /// <param name="record"></param>
    /// <returns></returns>
    public (bool[] cleared,bool newRecod) ChangeData
        (StageBook stageBook,ReadOnlyCollection<IEnemyInfo>  newlyKilledEnemies,int record)
    {
        //どの星の目標が達成できたか
        bool[] clears = Array.ConvertAll(stageBook.Goals,g=>g <= record);
        
        //今回のスコアがハイスコアか否か
        bool newRecord = (record != 0) && UserData.GetHighScore(stageBook.StageId) < record;
        
        //今回倒した敵の情報を追加する
        Dictionary<int, int> killedEnemies = UserData.Enemies_ReadOnly;
        foreach (var e in newlyKilledEnemies)
        {
            if (killedEnemies.ContainsKey(e.Book.EnemyId))
            {
                killedEnemies[e.Book.EnemyId]++;
            }
            else
            {
                killedEnemies.Add(e.Book.EnemyId,1);
            }
        }

        //新記録だった時更新
        var stageRecords = UserData.Records_ReadOnly;
        if (newRecord)
        {
            if (stageRecords.ContainsKey(stageBook.StageId))
            {
                stageRecords[stageBook.StageId] = record;
            }
            else
            {
                stageRecords.Add(stageBook.StageId,record);
            }
        }
        
        //クリアしたステージを更新
        var clearedStage = UserData.Cleared_ReadOnly;
        if (Array.Exists(clears,c=>c))
        {
            clearedStage.Add(stageBook.StageId);
        }

        UserData.ChangeRecord(killedEnemies,stageRecords,clearedStage);
        Save();
        
        return (clears,newRecord);
    }
    private void Save()
    {
        string jsonData = JsonUtility.ToJson(UserData);
        if (Setting.RUN_ON_ATUMARU)
        {
            PlayerPrefsAtsumaru.SetString(PREFS_KEY,jsonData);
            PlayerPrefsAtsumaru.Save();
        }
        else
        {
            PlayerPrefs.SetString(PREFS_KEY, jsonData);
            PlayerPrefs.Save();
        }

        
    }
    private void Load()
    {
        if (Setting.RUN_ON_ATUMARU)
        {
            
            if (PlayerPrefsAtsumaru.HasKey(PREFS_KEY))
            {
                UserData = JsonUtility.FromJson<UserData>(PlayerPrefsAtsumaru.GetString(PREFS_KEY));
            }
            else
            {
                UserData = new UserData();
                Save();
            }
        }
        else
        {

            if (PlayerPrefs.HasKey(PREFS_KEY))
            {
                UserData = JsonUtility.FromJson<UserData>(PlayerPrefs.GetString(PREFS_KEY));
            }
            else
            {
                UserData = new UserData();
                Save();
            }
        }
    }
    
    #if UNITY_EDITOR
    internal void EditData(string s)
    {
        UserData = JsonUtility.FromJson<UserData>(s);
    }
    #endif
}


