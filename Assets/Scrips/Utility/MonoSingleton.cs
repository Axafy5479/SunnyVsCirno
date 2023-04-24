using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T:MonoSingleton<T>
{
    
    
        
    #region Singleton

    protected static T instance;

    public static T I
    {
        get
        {
            if (instance == null)
            {
                var temp = FindObjectsOfType<T>();
                if (temp.Length == 0)
                {
                    Debug.LogError("Timerが見つかりませんでした");
                    return null;
                }
                else if (temp.Length > 1)
                {
                    Debug.LogError("Timerが複数見つかりました");
                    return null;
                }
                else
                {
                    instance = temp[0];
                }
            }

            return instance;
        }
    }

    #endregion

}
