using UnityEngine;

namespace Valkyrie.Components
{
	public class MonoInstance : MonoBehaviour
	{
		public static MonoInstance Instance { get; private set; }

		private void Awake()
		{
			if (Instance != this)
			{
				Destroy(Instance);
				Instance = this;
			}
		}
	}
}

