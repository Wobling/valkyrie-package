using System;
using System.Globalization;
using UnityEngine;

namespace Valkyrie.Extensions
{
	[Flags]
	public enum RGBA
	{
		None = 0,
		Red = 1,
		Green = 2,
		Blue = 4,
		Alpha = 8
	}

	public static class ColorExtensions
	{
		public static Vector4 ToVector4(this Color self)
		{
			return new Vector4(self.r, self.g, self.b, self.a);
		}

		/// <summary>
		/// Lerps between multiple colors
		/// </summary>
		public static Color Lerp(this Color[] self, float value)
		{
			value = Mathf.Clamp(value, 0, 1) * (self.Length - 1);
			var a = (int) value;
			int b = Mathf.Min((int) value + 1, self.Length - 1);
			return Color.Lerp(self[a], self[b], value - (int) value);
		}

		/// <summary>
		/// Move Towards implementation for Color
		/// </summary>
		public static Color MoveTowards(this Color self, Color b, float maxDelta)
		{
			var result = new Color
			{
				r = Mathf.MoveTowards(self.r, b.r, maxDelta),
				g = Mathf.MoveTowards(self.g, b.g, maxDelta),
				b = Mathf.MoveTowards(self.b, b.b, maxDelta),
				a = Mathf.MoveTowards(self.a, b.a, maxDelta)
			};

			self.r = result.r;
			self.g = result.g;
			self.b = result.b;
			self.a = result.a;

			return result;
		}

		/// <summary>
		/// Converts a color to a string formatted to C#
		/// </summary>
		public static string ToCSharpColor(this Color self)
		{
			return $"({TrimFloat(self.r)}f, {TrimFloat(self.g)}f, {TrimFloat(self.b)}f, {TrimFloat(self.a)}f)";
		}

		/// <summary>
		///  Pows the color with the specified factor
		/// </summary>
		public static Color Pow(this Color self, float factor)
		{
			self.r = Mathf.Pow(self.r, factor);
			self.g = Mathf.Pow(self.g, factor);
			self.b = Mathf.Pow(self.b, factor);
			self.a = Mathf.Pow(self.a, factor);
			return self;
		}
		
		/// <summary>
		/// Return 'color' where 'color'.r = red
		/// </summary>
		public static Color WithRed(this Color self, float red)
		{
			self.r = red;
			return self;
		}

		/// <summary>
		/// Return 'color' where 'color'.g = green
		/// </summary>
		public static Color WithGreen(this Color self, float green)
		{
			self.g = green;
			return self;
		}

		/// <summary>
		/// Return 'color' where 'color'.b = blue
		/// </summary>
		public static Color WithBlue(this Color self, float blue)
		{
			self.b = blue;
			return self;
		}
		
		/// <summary>
		/// Return 'color' where 'color'.a = alpha
		/// </summary>
		public static Color WithAlpha(this Color self, float alpha)
		{
			self.a = alpha;
			return self;
		}

		/// <summary>
		/// Return 'color' where 'color'.component = value.
		/// </summary>
		public static Color WithComponent(this Color self, RGBA component, float value)
		{
			switch (component)
			{
				case RGBA.Red:
					self.r = value;
					break;
				case RGBA.Green:
					self.g = value;
					break;
				case RGBA.Blue:
					self.b = value;
					break;
				case RGBA.Alpha:
					self.a = value;
					break;
				case RGBA.None:
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(component), component, null);
			}

			return self;
		}

		private static string TrimFloat(float value)
		{
			string floatString = value.ToString("F3", CultureInfo.InvariantCulture).TrimEnd('0');
			char lastChar = floatString[floatString.Length - 1];

			if (lastChar == '.' || lastChar == ',') 
				floatString = floatString.Substring(0, floatString.Length - 1);

			return floatString;
		}
	}
}