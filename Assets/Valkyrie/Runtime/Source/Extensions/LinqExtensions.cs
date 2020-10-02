using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Valkyrie.Extensions
{
	public static class LinqExtensions
	{
		private static readonly System.Random _shuffleRandom = new System.Random();

		/// <summary>
		/// Adds the element if the element is not already contained within the List.
		/// </summary>
		public static void AddIfUnique<T>(this List<T> self, T element)
		{
			if (!self.Contains(element))
				self.Add(element);
		}

		/// <summary>
		/// Adds the element if the element is not already contained within the Hashset.
		/// </summary>
		public static void AddIfUnique<T>(this HashSet<T> self, T element)
		{
			if (!self.Contains(element))
				self.Add(element);
		}

		/// <summary>
		/// Shuffle the elements in the <c>IList</c> using Fisher-Yates select and swap;
		/// </summary>
		public static void Shuffle<T>(this IList<T> self)
		{
			int n = self.Count;
			while (n > 1)
			{
				n--;
				int k = _shuffleRandom.Next(n + 1);
				T value = self[k];
				self[k] = self[n];
				self[n] = value;
			}
		}

		/// <summary>
		/// Returns a random element from an <c>IEnumerable</c>. Special case for 'int' type.
		/// Note: RandomOrDefault is much faster when used on a collection of 'int' or 'float'.
		/// Count &lt; 100 optimised from https://nickstrips.wordpress.com/2010/08/28/c-optimized-extension-method-get-a-random-element-from-a-collection/
		/// </summary>
		public static int RandomOrDefault(this IEnumerable<int> self)
		{
			IEnumerable<int> enumerable = self as int[] ?? self.ToArray();
			int count = enumerable.Count();
			int index = Random.Range(0, count);

			// When the collection has 100 elements or less, get the random element.
			// by traversing the collection one element at a time.
			if (count > 100) return enumerable.ElementAt(index);

			using (IEnumerator<int> enumerator = enumerable.GetEnumerator())
			{
				while (index >= 0 && enumerator.MoveNext()) index--;

				return enumerator.Current;
			}
		}

		/// <summary>
		/// Returns a random element from an <c>IEnumerable</c>. Special case for 'int' type.
		/// Note: RandomOrDefault is much faster when used on a collection of 'int' or 'float'.
		/// Count &lt; 100 optimised from https://nickstrips.wordpress.com/2010/08/28/c-optimized-extension-method-get-a-random-element-from-a-collection/
		/// </summary>
		public static float RandomOrDefault(this IEnumerable<float> self)
		{
			IEnumerable<float> enumerable = self as float[] ?? self.ToArray();
			int count = enumerable.Count();
			int index = Random.Range(0, count);

			// When the collection has 100 elements or less, get the random element.
			// by traversing the collection one element at a time.
			if (count > 100) return enumerable.ElementAt(index);

			using (IEnumerator<float> enumerator = enumerable.GetEnumerator())
			{
				while (index >= 0 && enumerator.MoveNext()) index--;

				return enumerator.Current;
			}
		}

		/// <summary>
		/// Returns a random element from an <c>IEnumerable</c>.
		/// </summary>
		public static T RandomOrDefault<T>(this IEnumerable<T> self)
		{
			T randomElement = default;
			int count = 0;

			foreach (T element in self)
			{
				++count;
				if (Random.value <= 1.0f / count) randomElement = element;
			}

			return randomElement;
		}

		/// <summary>
		/// Returns a random element from an <c>IEnumerable</c> which satifies the given predicate.
		/// </summary>
		/// <param name="self"> The <c>IEnumberable</c>.</param>
		/// <param name="predicate">A predicate which each element must satisfy.</param>
		/// <returns></returns>
		public static T RandomOrDefault<T>(this IEnumerable<T> self, Func<T, bool> predicate)
		{
			T randomElement = default;
			int count = 0;

			foreach (T element in self)
			{
				if (!predicate(element)) continue;

				++count;

				if (Random.value <= 1.0f / count) randomElement = element;
			}

			return randomElement;
		}

		/// <summary>
		/// Moves all elements which match 'Predicate' by 'offset'
		/// </summary>
		/// <param name="self"></param>
		/// <param name="itemSelector"></param>
		/// <param name="offset"></param>
		public static void Move<T>(this List<T> self, Predicate<T> itemSelector, int offset)
		{
			List<T> locatedItems = self.FindAll(itemSelector);

			foreach (T item in locatedItems)
			{
				int currentIndex = self.IndexOf(item);
				int nextIndex = currentIndex + offset;

				if (nextIndex > self.Count - 1 || nextIndex < 0 || currentIndex == -1) return;

				T currentItem = self[currentIndex];
				self.RemoveAt(currentIndex);
				self.Insert(nextIndex, currentItem);
			}
		}

		/// <summary>
		/// Calls an action on each item before yielding them.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		public static IEnumerable<T> Examine<T>(this IEnumerable<T> self, Action<T> action)
		{
			foreach (T item in self)
			{
				action(item);
				yield return item;
			}
		}

		/// <summary>
		/// Perform an action on each item.
		/// </summary>
		/// <param name="self"></param>
		/// <param name="action"></param>
		public static IEnumerable<T> ForEach<T>(this IEnumerable<T> self, Action<T> action)
		{
			IEnumerable<T> forEach = self as T[] ?? self.ToArray();
			foreach (T item in forEach)
				action(item);
			return forEach;
		}

		/// <summary>
		/// Perform an action on each item.
		/// </summary>
		/// <param name="self"></param>
		/// <param name="action"></param>
		public static IEnumerable<T> ForEach<T>(this IEnumerable<T> self, Action<T, int> action)
		{
			var counter = 0;
			IEnumerable<T> forEach = self as T[] ?? self.ToArray();
			foreach (T item in forEach) action(item, counter++);
			return forEach;
		}

		/// <summary>
		/// Converts each item in the collection
		/// </summary>
		/// <param name="self"></param>
		/// <param name="converter"></param>
		public static IEnumerable<T> Convert<T>(this IEnumerable<T> self, Func<object, T> converter)
		{
			foreach (T item in self) yield return converter(item);
		}

		/// <summary>
		/// Convert a collection to a HashSet
		/// </summary>
		public static HashSet<T> ToHashSet<T>(this IEnumerable<T> self)
		{
			return new HashSet<T>(self);
		}

		/// <summary>
		///  Add an item to the beginning of a collection.
		/// </summary>
		/// <param name="self"></param>
		/// <param name="prepend"></param>
		public static IEnumerable<T> Prepend<T>(this IEnumerable<T> self, Func<T> prepend)
		{
			yield return prepend();
			foreach (T item in self) yield return item;
		}

		/// <summary>
		///  Add an item to the beginning of a collection.
		/// </summary>
		/// <param name="self"></param>
		/// <param name="prepend"></param>
		public static IEnumerable<T> Prepend<T>(this IEnumerable<T> self, T prepend)
		{
			yield return prepend;
			foreach (T item in self) yield return item;
		}

		/// <summary>
		///  Add an item to the beginning of another collection, if a condition is met.
		/// </summary>
		/// <param name="self"></param>
		/// <param name="condition"></param>
		/// <param name="prepend"></param>
		public static IEnumerable<T> PrependIf<T>(this IEnumerable<T> self, bool condition, Func<T> prepend)
		{
			if (condition) yield return prepend();
			foreach (T item in self) yield return item;
		}

		/// <summary>
		/// Add an item to the beginning of another collection, if a condition is met.
		/// </summary>
		/// <param name="self"></param>
		/// <param name="condition"></param>
		/// <param name="prepend"></param>
		public static IEnumerable<T> PrependIf<T>(this IEnumerable<T> self, bool condition, T prepend)
		{
			if (condition) yield return prepend;

			foreach (T item in self) yield return item;
		}

		/// <summary>
		/// Add an item to the beginning of another collection, if a condition is met.
		/// </summary>
		/// <param name="self"></param>
		/// <param name="condition"></param>
		/// <param name="prepend"></param>
		public static IEnumerable<T> PrependIf<T>(this IEnumerable<T> self, bool condition, IEnumerable<T> prepend)
		{
			if (condition)
				foreach (T item in prepend)
					yield return item;

			foreach (T item in self) yield return item;
		}

		/// <summary>
		/// Add an item to the beginning of another collection, if a condition is met.
		/// </summary>
		/// <param name="self"></param>
		/// <param name="condition"></param>
		/// <param name="prepend"></param>
		public static IEnumerable<T> PrependIf<T>(this IEnumerable<T> self, Func<bool> condition, Func<T> prepend)
		{
			if (condition()) yield return prepend();

			foreach (T item in self) yield return item;
		}

		/// <summary>
		/// Add an item to the beginning of another collection, if a condition is met.
		/// </summary>
		/// <param name="self"></param>
		/// <param name="condition"></param>
		/// <param name="prepend"></param>
		public static IEnumerable<T> PrependIf<T>(this IEnumerable<T> self, Func<bool> condition, T prepend)
		{
			if (condition()) yield return prepend;

			foreach (T item in self) yield return item;
		}

		/// <summary>
		/// Add an item to the beginning of another collection, if a condition is met.
		/// </summary>
		/// <param name="self"></param>
		/// <param name="condition"></param>
		/// <param name="prepend"></param>
		public static IEnumerable<T> PrependIf<T>(this IEnumerable<T> self, Func<bool> condition, IEnumerable<T> prepend)
		{
			if (condition())
				foreach (T item in prepend)
					yield return item;

			foreach (T item in self) yield return item;
		}

		/// <summary>
		/// Add an item to the beginning of another collection, if a condition is met.
		/// </summary>
		/// <param name="self"></param>
		/// <param name="condition"></param>
		/// <param name="prepend"></param>
		public static IEnumerable<T> PrependIf<T>(this IEnumerable<T> self, Func<IEnumerable<T>, bool> condition, Func<T> prepend)
		{
			IEnumerable<T> enumerable = self as T[] ?? self.ToArray();
			if (condition(enumerable)) yield return prepend();

			foreach (T item in enumerable) yield return item;
		}

		/// <summary>
		/// Add an item to the beginning of another collection, if a condition is met.
		/// </summary>
		/// <param name="self"></param>
		/// <param name="condition"></param>
		/// <param name="prepend"></param>
		public static IEnumerable<T> PrependIf<T>(this IEnumerable<T> self, Func<IEnumerable<T>, bool> condition, T prepend)
		{
			IEnumerable<T> enumerable = self as T[] ?? self.ToArray();
			if (condition(enumerable)) yield return prepend;

			foreach (T item in enumerable) yield return item;
		}

		/// <summary>
		/// Add an item to the beginning of another collection, if a condition is met.
		/// </summary>
		/// <param name="self"></param>
		/// <param name="condition"></param>
		/// <param name="prepend"></param>
		public static IEnumerable<T> PrependIf<T>(this IEnumerable<T> self, Func<IEnumerable<T>, bool> condition, IEnumerable<T> prepend)
		{
			IEnumerable<T> enumerable = self as T[] ?? self.ToArray();
			if (condition(enumerable))
				foreach (T item in prepend)
					yield return item;

			foreach (T item in enumerable) yield return item;
		}

		/// <summary>
		/// Add an item to the end of a collection.
		/// </summary>
		/// <param name="self"></param>
		/// <param name="append"></param>
		public static IEnumerable<T> Append<T>(this IEnumerable<T> self, Func<T> append)
		{
			foreach (T item in self) yield return item;

			yield return append();
		}

		/// <summary>
		/// Add an item to the end of a collection.
		/// </summary>
		/// <param name="self"></param>
		/// <param name="append"></param>
		public static IEnumerable<T> Append<T>(this IEnumerable<T> self, T append)
		{
			foreach (T item in self) yield return item;

			yield return append;
		}

		/// <summary>
		/// Add an item to the end of a collection.
		/// </summary>
		/// <param name="self"></param>
		/// <param name="append"></param>
		public static IEnumerable<T> Append<T>(this IEnumerable<T> self, IEnumerable<T> append)
		{
			foreach (T item in self) yield return item;

			foreach (T item in append) yield return item;
		}

		/// <summary>
		/// Add an item to the end of a collection if a condition is met
		/// </summary>
		/// <param name="self"></param>
		/// <param name="condition"></param>
		/// <param name="append"></param>
		public static IEnumerable<T> AppendIf<T>(this IEnumerable<T> self, bool condition, Func<T> append)
		{
			foreach (T item in self) yield return item;

			if (condition) yield return append();
		}

		/// <summary>
		/// Add an item to the end of a collection if a condition is met
		/// </summary>
		/// <param name="self"></param>
		/// <param name="condition"></param>
		/// <param name="append"></param>
		public static IEnumerable<T> AppendIf<T>(this IEnumerable<T> self, bool condition, T append)
		{
			foreach (T item in self) yield return item;

			if (condition) yield return append;
		}

		/// <summary>
		/// Add an item to the end of a collection if a condition is met
		/// </summary>
		/// <param name="self"></param>
		/// <param name="condition"></param>
		/// <param name="append"></param>
		public static IEnumerable<T> AppendIf<T>(this IEnumerable<T> self, bool condition, IEnumerable<T> append)
		{
			foreach (T item in self) yield return item;

			if (!condition) yield break;

			foreach (T item in append) yield return item;
		}

		/// <summary>
		/// Add an item to the end of a collection if a condition is met
		/// </summary>
		/// <param name="self"></param>
		/// <param name="condition"></param>
		/// <param name="append"></param>
		public static IEnumerable<T> AppendIf<T>(this IEnumerable<T> self, Func<bool> condition, Func<T> append)
		{
			foreach (T item in self) yield return item;

			if (condition()) yield return append();
		}

		/// <summary>
		/// Add an item to the end of a collection if a condition is met
		/// </summary>
		/// <param name="self"></param>
		/// <param name="condition"></param>
		/// <param name="append"></param>
		public static IEnumerable<T> AppendIf<T>(this IEnumerable<T> self, Func<bool> condition, T append)
		{
			foreach (T item in self) yield return item;

			if (condition()) yield return append;
		}

		/// <summary>
		/// Add an item to the end of a collection if a condition is met
		/// </summary>
		/// <param name="self"></param>
		/// <param name="condition"></param>
		/// <param name="append"></param>
		public static IEnumerable<T> AppendIf<T>(this IEnumerable<T> self, Func<bool> condition, IEnumerable<T> append)
		{
			foreach (T item in self) yield return item;

			if (!condition()) yield break;
			foreach (T item in append) yield return item;
		}

		/// <summary>
		/// Returns and casts only the items of type <typeparamref name="T"/>.
		/// </summary>
		public static IEnumerable<T> FilterCast<T>(this IEnumerable<T> self)
		{
			foreach (T obj in self)
				if (obj is T)
					yield return obj;
		}

		/// <summary>
		/// Adds a collection to a hashset.
		/// </summary>
		/// <param name="self"></param>
		/// <param name="range"></param>
		public static void AddRange<T>(this HashSet<T> self, IEnumerable<T> range)
		{
			foreach (T value in range) self.Add(value);
		}

		/// <summary>
		/// Returns <c>true</c> if the list is either null or empty. Otherwise <c>false</c>.
		/// </summary>
		public static bool IsNullOrEmpty<T>(this IList<T> self)
		{
			return self == null || self.Count == 0;
		}

		/// <summary>
		/// Sets all items in the list to the given value
		/// </summary>
		/// <param name="self"></param>
		/// <param name="item"></param>
		public static void Populate<T>(this IList<T> self, T item)
		{
			int count = self.Count;
			for (var i = 0; i < count; i++) self[i] = item;
		}
	}
}