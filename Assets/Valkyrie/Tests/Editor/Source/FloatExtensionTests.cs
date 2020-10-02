using NUnit.Framework;
using Valkyrie.Extensions;

namespace Tests
{
	public class FloatExtensionTests
	{

		[TestCase(0.01f, ExpectedResult = 0)]
		[TestCase(0.75f, ExpectedResult = 1)]
		[TestCase(0.5f, ExpectedResult = 0)]
		public float Round_RoundFloatToNearestIntFloat_ReturnsTheNearestIntFloat(float input)
		{
			return input.Round();
		}
		
		[TestCase(0.6f, 2.0f, ExpectedResult = 0.5f)]
		[TestCase(1.75f, 1.5f, ExpectedResult = 2.0f)]
		public float Round_RoundFloatToNearestSnapFloat_ReturnsTheNearestFloat(float input, float increment)
		{
			return input.Round(increment);
		}
	}
}