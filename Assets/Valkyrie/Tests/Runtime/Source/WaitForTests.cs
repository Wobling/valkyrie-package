using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Valkyrie.Components;
using Valkyrie.Timers;

namespace Tests
{
    public class WaitForTests
    {
        [UnityTest]
        public IEnumerator WaitFor_Frames_TwoFramesElapse()
        {
            // Arrange
            var gameObject = new GameObject();
            var monoInstance = gameObject.AddComponent<MonoInstance>();
            var framesElapsed = false;
            
            // Act
            monoInstance.StartCoroutine(WaitFor.Frames(2, () =>
            {
                framesElapsed = true;
            }));
            
            // Assert
            yield return null;
            yield return null;
            
            Assert.AreEqual(true, framesElapsed);
        }
    }
}
