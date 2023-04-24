using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GoalData : ScriptableObject
{
    [SerializeField] private int[] goals;

    public int[] Goals => goals;
}
