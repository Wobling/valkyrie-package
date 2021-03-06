﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valkyrie.Timers
{
    public class MonoTimer : MonoBehaviour
    {
        private float _duration;
        private bool _isRealTime;
        private readonly WaitForSeconds _waitForSeconds = new WaitForSeconds(1.0f);
        private readonly WaitForSecondsRealtime _waitForSecondsRealtime = new WaitForSecondsRealtime(1.0f);
        private float _elapsedTime;
        private Coroutine _coroutine;

        /// <summary>
        /// Must be called before the StartTimer function
        /// Duration provided in seconds
        /// IsRealTime determines if this timer is affected by TimeScale changes
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="isRealTime"></param>
        public void CreateTimer(float duration, bool isRealTime = false)
        {
            _duration = duration;
            _isRealTime = isRealTime;
        }
        
        /// <summary>
        /// Begins the timer execution
        /// </summary>
        /// <param name="onCompleted"></param>
        /// <param name="onTick"></param>
        public void StartTimer(Action onCompleted, Action onTick = null)
        {
            _coroutine = StartCoroutine(TimerRoutine(onCompleted, onTick));
        }

        /// <summary>
        /// Stops the timer execution
        /// </summary>
        public void StopTimer()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                Debug.Log("Timer stopped before completion");
            }
            else
            {
                Debug.Log("Attempted to stop a timer that doesn't exist.");
            }
        }

        private IEnumerator TimerRoutine(Action onCompleted, Action onTick = null)
        {
            while (_elapsedTime < _duration )
            {
                if (_isRealTime)
                {
                    yield return _waitForSecondsRealtime;
                }
                else
                {
                    yield return _waitForSeconds;
                }
                
                _elapsedTime++;
                onTick?.Invoke();
            }
            
            onCompleted?.Invoke();
        }
    }
}


