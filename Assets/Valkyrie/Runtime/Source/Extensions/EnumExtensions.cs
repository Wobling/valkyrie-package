using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Valkyrie.Extensions
{
	//----------------------------------------------------------------------------------------------
	// Attribute Declaration:
	//----------------------------------------------------------------------------------------------

	public class FlagDrawerAttribute : PropertyAttribute { }

	public static class EnumExtensions 
	{
		/// <summary>
		/// Returns true when the passed flag is set to 'true' on this enum
		/// </summary>
		public static bool IsFlagSet(this Enum self, Enum flag) 
		{
			return (Convert.ToInt64(self) & Convert.ToInt64(flag)) != 0;
		}

		/// <summary>
		/// Returns true when the passed int flag/s are set to 'true' on this enum
		/// </summary>
		public static bool IsFlagSet(this Enum self, int flag) 
		{
			return (Convert.ToInt64(self) & flag) != 0;
		}

		/// <summary>
		/// Returns true when the passed flag/s are set to 'true' on this enum
		/// </summary>
		public static bool IsFlagSet<T>(this T self, T flag) where T : struct, IConvertible 
		{
			IsEnum<T>(true);
			return (Convert.ToInt64(self) & Convert.ToInt64(flag)) != 0;
		}

		/// <summary>
		/// Returns true when the passed int flag/s are set to 'true' on this enum
		/// </summary>
		public static bool IsFlagSet<T>(this T self, int flag) where T : struct, IConvertible 
		{
			IsEnum<T>(true);
			return (Convert.ToInt64(self) & flag) != 0;
		}

		/// <summary>
		/// Returns true when this int 'flag/s' has the flag/s 'flag' set to 'true'.
		/// </summary>
		public static bool IsFlagSet<T>(this int self, int flag) where T : struct, IConvertible
		{
			IsEnum<T>(true);
			return (self & flag) != 0;
		}

		/// <summary>
		/// Returns true when this enum has any flag matches with 'flag'.
		/// </summary>
		public static bool AnyFlagMatches<T>(this T self, T flag) where T : struct, IConvertible
		{
			IsEnum<T>(true);
			return flag.GetFlags().Any(f => self.IsFlagSet(f));
		}

		/// <summary>
		/// Returns a collection of all flags which are set to 'true' on this enum.
		/// </summary>
		public static IEnumerable<T> GetFlags<T>(this T self) where T : struct, IConvertible 
		{
			IsEnum<T>(true);
			return Enum.GetValues(typeof(T)).Cast<T>().Where(f => self.IsFlagSet(f));
		}

		/// <summary>
		/// Set flag/s to 'on' on this enum.
		/// </summary>
		public static T SetFlags<T>(this T self, T flags, bool on) where T : struct, IConvertible 
		{
			IsEnum<T>(true);
			long lValue = Convert.ToInt64(self);
			long lFlag = Convert.ToInt64(flags);

			if (on)
				lValue |= lFlag;
			else
				lValue &= ~lFlag;

			return (T) Enum.ToObject(typeof(T), lValue);
		}

		/// <summary>
		/// Combines a collection of flags together additively.
		/// </summary>
		public static T CombineFlags<T>(this IEnumerable<T> self) where T : struct, IConvertible 
		{
			IsEnum<T>(true);
			long lValue = 0;
			
			foreach (T flag in self)
			{
				long lFlag = Convert.ToInt64(flag);
				lValue |= lFlag;
			}

			return (T) Enum.ToObject(typeof(T), lValue);
		}

		private static void IsEnum<T>(bool withFlags) 
		{
			if (!typeof(T).IsEnum) throw new ArgumentException($"Type '{typeof(T).FullName}' is not an enum");
			if (withFlags && !Attribute.IsDefined(typeof(T), typeof(FlagsAttribute)))
				throw new ArgumentException($"Type '{typeof(T).FullName}' doesn't have the 'Flags' attribute");
		}
	}
}