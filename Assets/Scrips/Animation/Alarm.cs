using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scrips.Animation
{
    public class Alarm : Timer
    {
        public float GoalTime { get; private set; }
        private Action OnTime { get; set; }
        
        public void AlarmStart(float time, Action onTime)
        {
            GoalTime = time;
            OnTime = onTime;
            Restart();
        }
        
        protected override void Update()
        {
            if (!IsRunning) return;

            base.Update();

            if (GoalTime <= Current)
            {
                OnTime();
                
                Stop();
            }
        }

        public override void Reset()
        {
            base.Reset();

            OnTime = null;
        }
    }
}
