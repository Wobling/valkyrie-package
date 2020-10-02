using UnityEngine;

namespace Valkyrie.Extensions
{
	public static class GraphicsExtensions
	{
		/// <summary>
		/// Attempts to determine if a texture is readable by reading a single pixel.
		/// </summary>
		public static bool IsReadable(this Texture2D self)
		{
			try
			{
				self.GetPixel(0, 0);
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// Returns the Vector2Int (width, height)
		/// </summary>
		public static Vector2Int Size(this Texture self)
		{
			return new Vector2Int(self.width, self.height);
		}

		/// <summary>
		/// Converts a RenderTexture to a Texture2D. Warning: This is expensive.
		/// </summary>
		public static Texture2D ToTexture2D(this RenderTexture self, bool mipmap = false,
		                                    bool linear = true)
		{
			var returnTexture = new Texture2D(self.width, self.height, TextureFormat.RGBA32,
				mipmap, linear);
			
			RenderTexture previousTexture = RenderTexture.active;
			RenderTexture.active = self;
			returnTexture.ReadPixels(new Rect(0, 0, returnTexture.width, returnTexture.height), 0, 0);
			returnTexture.Apply();
			RenderTexture.active = previousTexture;
			return returnTexture;
		}
		
		/// <summary>
		/// Converts a Sprite to a baked Texture2D.
		/// </summary>
		public static Texture2D ToTexture2D(this Sprite self)
		{
			Rect spriteRect = self.rect;
			var position = new Vector2Int((int) spriteRect.x, (int) spriteRect.y);
			var size = new Vector2Int((int) spriteRect.width, (int) spriteRect.height);

			var returnTexture = new Texture2D(size.x, size.x);

			Color[] pixels =
				self.texture.GetPixels(position.x, position.y, size.x, size.y);
			returnTexture.SetPixels(pixels);
			returnTexture.Apply();
			returnTexture.name = self.name;
			return returnTexture;
		}

		/// <summary>
		/// Fill all pixels of a Texture2D with 'color'
		/// </summary>
		public static void FloodFill(this Texture2D self, Color color)
		{
			for (var x = 0; x < self.width; ++x)
			{
				for (var y = 0; y < self.height; ++y)
				{
					self.SetPixel(x, y, color);
				}
			}
		}
	}
}