using System.Drawing;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using Valkyrie.Extensions;

namespace Tests
{
	public class GraphicsExtensionTests
	{
		[Test]
		public void IsReadable_IsTextureReadable_TextureIsReadCorrectly()
		{
			var texture = new Texture2D(100, 100);
			
			bool result = texture.IsReadable();
			
			Assert.That(result);
		}

		[Test]
		public void Size_ReturnTextureSizeAsVector2Int_ReturnsVector2Int()
		{
			var texture = new Texture2D(50, 100);
			
			var expectedResult = new Vector2Int(50, 100);

			Vector2Int size = texture.Size();
			bool result = size == expectedResult;
			
			Assert.That(result);
		}

		[Test]
		public void ToTexture2D_ConvertRenderTextureToTexture2D_ReturnsTexture2D()
		{
			var renderTexture = new RenderTexture(100, 100, 0, RenderTextureFormat.ARGB32);

			Texture2D texture2D = renderTexture.ToTexture2D(false, false);

			Assert.That(texture2D != null);
		}
		
		[Test]
		public void ToTexture2D_ConvertRenderTextureToTexture2D_ReturnsTexture2DWithMipmap()
		{
			var renderTexture = new RenderTexture(100, 100, 0, RenderTextureFormat.ARGB32);

			Texture2D texture2D = renderTexture.ToTexture2D(true, false);

			Assert.That(texture2D != null);
		}
		
		[Test]
		public void ToTexture2D_ConvertRenderTextureToTexture2D_ReturnsTexture2DWithLinear()
		{
			var renderTexture = new RenderTexture(100, 100, 0, RenderTextureFormat.ARGB32);

			Texture2D texture2D = renderTexture.ToTexture2D(false, true);

			Assert.That(texture2D != null);
		}

		[Test]
		public void FloodFill_FillTextureWithColor_FillsEntireTexture2DWithSpecifiedColor()
		{
			var texture2D = new Texture2D(32, 32);
			
			texture2D.FloodFill(Color.red);
			Color color = texture2D.GetPixel(24, 10);

			bool result = color == Color.red;
			
			Assert.That(result);
		}
	}
}