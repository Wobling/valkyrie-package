using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Valkyrie.Timers;

namespace Tests
{
    public class MonoTimerTests
    {
        [UnityTest]
        public IEnumerator MonoTimer_StartTimer_TwoSecondsElapse()
        {
            // Arrange
            var gameObject = new GameObject();
            var monoTimer = gameObject.AddComponent<MonoTimer>();
            var timerElapsed = false;

            // Act
            monoTimer.CreateTimer(2.0f, true);
            monoTimer.StartTimer(() =>
            {
                timerElapsed = true;
            });
            
            yield return new WaitForSeconds(2.1f);

            // Assert
            Assert.AreEqual(true, timerElapsed);
        }
        
        [UnityTest]
        public IEnumerator MonoTimer_StopTimer_StopsBeforeTimerElapses()
        {
            // Arrange
            var gameObject = new GameObject();
            var monoTimer = gameObject.AddComponent<MonoTimer>();
            var timerElapsed = false;

            // Act
            monoTimer.CreateTimer(2.0f, true);
            monoTimer.StartTimer(() =>
            {
                timerElapsed = true;
            });
            
            yield return new WaitForSeconds(1);
            monoTimer.StopTimer();
            
            // Assert
            Assert.AreEqual(false, timerElapsed);
        }
    }
}
