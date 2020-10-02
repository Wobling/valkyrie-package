using System;
using System.Collections;

namespace Valkyrie.Timers
{
	public static class WaitFor
	{
		public static IEnumerator Frames(int frames, Action callback)
		{
			while (frames > 0)
			{
				frames--;
				yield return null;
			}
			
			callback?.Invoke();
		}
	}
}
