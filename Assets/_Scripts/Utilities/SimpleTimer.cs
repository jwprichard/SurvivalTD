using System;
using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public class SimpleTimer : MonoBehaviour
    {
        public delegate void EventHandler(object sender, EventArgs e);
        public event EventHandler<EventArgs> OnTimerElapsed = delegate { };

        public bool isRunning;

        public bool autoReset;
        public float timer;
        public float time;

        protected void OnApplicationQuit()
        {
            Destroy(gameObject);
        }

        public virtual void Init(float time, bool autoReset = false)
        {
            timer = time;
            this.time = timer;
            isRunning = true;
            this.autoReset = autoReset;
        }

        public void Update()
        {
            if (isRunning)
            {
                time -= Time.deltaTime;
                if (time < 0 && isRunning)
                {
                    TimerFinished();
                    if (autoReset)
                    {
                        time = timer;
                        isRunning = true;
                    }
                }
            }
        }

        private void TimerFinished()
        {
            isRunning = false;
            OnTimerElapsed.Invoke(this, new EventArgs());
        }
    }

    public class AttackTimer : SimpleTimer
    {
        public override void Init(float aps, bool autoReset = false)
        {
            // This returns attack speed in attacks per second. e.i. if the attack rate is 2 attacks per second
            // the timer will result to 0.5s which is 2 attcks per second.
            timer = 1/ aps;
            this.time = timer;
            isRunning = true;
            this.autoReset = autoReset;
        }
    }
}