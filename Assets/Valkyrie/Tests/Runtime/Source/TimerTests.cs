using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Valkyrie.Components;
using Valkyrie.Timers;

namespace Tests
{
    public class TimerTests
    {
        [UnityTest]
        public IEnumerator Timer_StartTimer_TwoSecondsElapse()
        {
            // Arrange
            var gameObject = new GameObject();
            var monoInstance = gameObject.AddComponent<MonoInstance>();
            
            var timer = new Timer(monoInstance, 2.0f);
            var timerElapsed = false;
            
            // Act
            timer.StartTimer(() => { timerElapsed = true; });
            yield return new WaitForSeconds(2.1f);
            
            // Assert
            Assert.AreEqual(true, timerElapsed);
        }
        
        [UnityTest]
        public IEnumerator Timer_StopTimer_StopsBeforeTimerElapses()
        {
            // Arrange
            var gameObject = new GameObject();
            var monoInstance = gameObject.AddComponent<MonoInstance>();
            
            var timer = new Timer(monoInstance, 2.0f);
            var timerElapsed = false;

            // Act
            timer.StartTimer(() => { timerElapsed = true; });
            yield return new WaitForSeconds(1.0f);
            timer.StopTimer();
            
            // Assert
            Assert.AreEqual(false, timerElapsed);
        }
    }
}
