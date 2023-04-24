using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lilly : EnemyView
{
    [SerializeField] private float speed = 1;
    private Transform Trn { get;  set; }
    protected override void Start()
    {
        base.Start();
        Trn = this.transform;
    }

    private void Update()
    {
        Trn.Translate(speed*1.5f*Vector3.up*Time.deltaTime);
    }
}
