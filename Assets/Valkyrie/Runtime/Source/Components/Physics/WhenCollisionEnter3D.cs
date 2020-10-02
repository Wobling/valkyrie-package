using System;
using UnityEngine;

namespace Valkyrie.Components.Physics
{
	public class WhenCollisionEnter3D : MonoBehaviour
	{
		public event Action<Collision> OnCollisionEntered;

		private void OnCollisionEnter(Collision other)
		{
			OnCollisionEntered?.Invoke(other);
		}
	}
}