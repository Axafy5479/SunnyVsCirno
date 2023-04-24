using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scrips.Animation
{
    public class Timer : MonoBehaviour
    {
        public float Current { get; private set; }
        public bool IsRunning { get; private set; }

        public void TimerStart()
        {
            IsRunning = true;
        }



        public void ResetAndStop()
        {
            Stop();
            Current = 0;
        }

        public void Restart()
        {
            Current = 0;
            TimerStart();
        }

        public void Stop()
        {
            IsRunning = false;
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if (IsRunning)
            {
                Current += Time.deltaTime;
            }
        }

        public virtual void Reset()
        {
            Stop();
            Current = 0;
        }
    }
}
