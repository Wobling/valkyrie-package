using NUnit.Framework;
using UnityEditor.VersionControl;
using UnityEngine;
using Valkyrie.Extensions;

namespace Tests
{
    /*
    Naming your tests

    The name of your test should consist of three parts:

    The name of the method being tested.
    The scenario under which it's being tested.
    The expected behavior when the scenario is invoked.

     */
    public class ColorExtensionTests
    {
        [Test]
        public void ToVector4_ColorToVector4_ReturnsColorAsVector4()
        {
            Color color = Color.red;
            var expectedResult = new Vector4(1, 0, 0, 1);
            
            Vector4 colorVector4 = color.ToVector4();
            bool equal = colorVector4 == expectedResult;

            Assert.That(equal);
        }

        [Test]
        public void Lerp_LerpColor_ColorChangedByFactor()
        {
            Color[] colors = {Color.red, Color.blue};
            var expectedColor = new Color(.5f, 0, .5f, 1);
            
            Color color =  ColorExtensions.Lerp(colors, .5f);
            bool equal = color == expectedColor;

            Assert.That(equal);
        }

        [Test]
        public void MoveTowards_ColorChangeRedToBlue_ReturnsBlue()
        {
            Color startColor = Color.red;
            Color expectedColor = Color.blue;
            
            startColor = startColor.MoveTowards(Color.blue, 1);
            bool equals = startColor == expectedColor;
            
            Assert.That(equals);
        }

        [Test]
        public void ToCSharpColor_ColorToFormattedString_ReturnsFormattedStringOfColor()
        {
            Color color = Color.blue;            
            Assert.That(color.ToCSharpColor() == "(0f, 0f, 1f, 1f)");
        }

        [Test]
        public void Pow_RaisesColorByPower_ReturnsRaisedColor()
        {
            var startColor = new Color(0.25f, 0, 0, 0);
            var expectedColor = new Color(0.5f, 0, 0, 0);

            startColor = startColor.Pow(0.5f);
            bool equals = startColor == expectedColor;
            
            Assert.That(equals);
        }
        
        [Test]
        public void WithRed_ColorIsReturnedWithFullRedChannel_ReturnedFullRedChannel()
        {
            Color startColor = Color.blue;
            var expectedResult = new Color(1, 0, 1, 1);

            startColor = startColor.WithRed(1.0f);
            bool equals = Mathf.Approximately(startColor.r, expectedResult.r);
            
            Assert.That(equals);
        }
        
        [Test]
        public void WithGreen_ColorIsReturnedWithFullGreenChannel_ReturnedFullGreenChannel()
        {
            Color startColor = Color.red;
            var expectedResult = new Color(0, 1, 0, 1);

            startColor = startColor.WithGreen(1.0f);
            bool equals = Mathf.Approximately(startColor.g, expectedResult.g);
            
            Assert.That(equals);
        }

        [Test]
        public void WithBlue_ColorIsReturnedWithFullBlueChannel_ReturnedFullBlueChannel()
        {
            Color startColor = Color.green;
            var expectedResult = new Color(0, 0, 1, 1);

            startColor = startColor.WithBlue(1.0f);
            bool equals = Mathf.Approximately(startColor.b, expectedResult.b);
            
            Assert.That(equals);
        }
        
        [Test]
        public void WithAlpha_ColorIsReturnedWithHalfAlpha_ReturnedHalfTransparentColor()
        {
            Color startColor = Color.red;
            var expectedResult = new Color(1, 0, 0, 0.5f);

            startColor = startColor.WithAlpha(0.5f);
            bool equals = Mathf.Approximately(startColor.a, expectedResult.a);
            
            Assert.That(equals);
        }

        [Test]
        public void WithComponent_ColorIsReturnedWithFullBlueChannel_ReturnedFullBlueChannel()
        {
            Color startColor = Color.red;
            var expectedResult = new Color(1, 0, 0.5f, 1);
            
            startColor = startColor.WithComponent(RGBA.Blue, 0.5f);
            bool equals = startColor == expectedResult;
            
            Assert.That(equals);
        }
    }
}
