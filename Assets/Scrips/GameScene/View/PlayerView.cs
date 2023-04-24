using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scrips.GameScene.View
{
    public class PlayerView : MonoSingleton<PlayerView>
    {
        [SerializeField] private float omega = 1;
        private Scrips.Animation.Timer timer;
        private float minmax = 2.79f;
        
        private Transform Trn { get; set; }
        public bool IsRunning => !LaunchBulletButton.I.IsPressed && StageView.I.IsRunning.Value;

        
        // Start is called before the first frame update
        void Start()
        {
            Trn = this.transform;
            timer = this.GetComponent<Scrips.Animation.Timer>();
        }

        private void Update()
        {
            if (IsRunning)
            {
                timer.TimerStart();
            }
            else
            {
                timer.Stop();
            }
            
            Vector3 pos = Trn.position;
            float y = minmax * (float)Mathf.Sin(omega * timer.Current);
            Trn.position = new Vector3(pos.x, y, pos.z);
        }
        
    }
}
