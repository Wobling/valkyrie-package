using NUnit.Framework;
using UnityEngine;
using Valkyrie.Extensions;

namespace Tests
{
	public class VectorExtensionTests
	{
		[Test]
		public void ToVector2Int_ConvertVector2ToVector2Int_ReturnsARoundedVector2Int()
		{
			var vector2 = new Vector2(0.75f, 0.4f);
			var expectedResult = new Vector2Int(1, 0);

			Vector2Int vector2Int = vector2.ToVector2Int();

			bool result = vector2Int == expectedResult;
			
			Assert.That(result);
		}

		[Test]
		public void ToVector2_ConvertVector2IntToVector2_ReturnsVector2()
		{
			var vector2Int = new Vector2Int(40, 100);
			var expectedResult = new Vector2(40.0f, 100.0f);

			Vector2 vector2 = vector2Int.ToVector2();
			bool result = vector2 == expectedResult;
			
			Assert.That(result);
		}

		[Test]
		public void ToVector3Int_ConvertVector3ToVector3Int_ReturnsARoundedVector3Int()
		{
			var vector3 = new Vector3(0.6f, 1.0f, 0.0f);
			var expectedResult = new Vector3Int(1, 1, 0);

			Vector3Int vector3Int = vector3.ToVector3Int();
			bool result = vector3Int == expectedResult;
			
			Assert.That(result);
		}
		
		[Test]
		public void ToVector3_ConvertVector3IntToVector3_ReturnsAVector3()
		{
			var vector3Int = new Vector3(10, 100, 1000);
			var expectedResult = new Vector3(10.0f, 100.0f, 1000.0f);

			Vector3 vector3 = vector3Int.ToVector3Int();
			bool result = vector3 == expectedResult;
			
			Assert.That(result);
		}

		[Test]
		public void ApproximateVector2_VectorAIsApproximateEqualToVectorB_VectorsAreEqual()
		{
			var vectorA = new Vector2(0.5f, 1.75f);
			var vectorB = new Vector2(0.5f, 1.75f);

			bool result = vectorA.Approximate(vectorB);
			
			Assert.That(result);
		}
		
		[Test]
		public void ApproximateVector3_VectorAIsApproximateEqualToVectorB_VectorsAreEqual()
		{
			var vectorA = new Vector3(0.5f, 1.75f, 1.0f);
			var vectorB = new Vector3(0.5f, 1.75f, 1.0f);

			bool result = vectorA.Approximate(vectorB);
			
			Assert.That(result);
		}
	}
}