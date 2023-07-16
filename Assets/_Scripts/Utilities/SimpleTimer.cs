using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Utilities
{
    public class SimpleTimer : MonoBehaviour
    {
        public UnityAction OnTimerElapsed = delegate { };

        public bool isRunning;

        public bool autoReset;
        public float Timer { get; private set; }
        public float Time { get; private set; }

        protected void OnApplicationQuit()
        {
            Destroy(gameObject);
        }

        public virtual void Init(float time, bool autoReset = false)
        {
            Timer = time;
            this.Time = Timer;
            isRunning = true;
            this.autoReset = autoReset;
        }

        public void Update()
        {
            if (isRunning)
            {
                Time -= UnityEngine.Time.deltaTime;
                if (Time < 0 && isRunning)
                {
                    TimerFinished();
                    if (autoReset)
                    {
                        Time = Timer;
                        isRunning = true;
                    }
                }
            }
        }

        private void TimerFinished()
        {
            isRunning = false;
            OnTimerElapsed.Invoke();
        }
    }

    public class AttackTimer : SimpleTimer
    {
        public new float Timer { get; private set; }
        public override void Init(float aps, bool autoReset = false)
        {
            // This returns attack speed in attacks per second. e.i. if the attack rate is 2 attacks per second
            // the timer will result to 0.5s which is 2 attacks per second.
            Timer = 1 / aps;

            base.Init(Timer, autoReset);

        }
    }
}