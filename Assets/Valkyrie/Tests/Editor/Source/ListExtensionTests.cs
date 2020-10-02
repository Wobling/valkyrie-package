using System.Collections.Generic;
using NUnit.Framework;
using Valkyrie.Extensions;

namespace Tests
{
    public class ListExtensionTests
    {
        [Test]
        public void IsEmpty_IsTheListCountEqualToZero_ReturnsBoolean()
        {
            var tempList = new List<int>();

            bool result = tempList.IsEmpty();
            
            Assert.AreEqual(result, true);
        }
    }
}