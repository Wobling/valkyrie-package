using System;
using UnityEngine;

namespace Valkyrie.Components.Physics
{
	public class WhenTriggerEnter3D : MonoBehaviour
	{
		public event Action<Collider> OnTriggerEntered;

		private void OnTriggerEnter(Collider other)
		{
			OnTriggerEntered?.Invoke(other);
		}
	}
}