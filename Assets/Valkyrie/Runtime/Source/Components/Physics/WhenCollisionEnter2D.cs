using System;
using UnityEngine;

namespace Valkyrie.Components.Physics
{
	public class WhenCollisionEnter2D : MonoBehaviour
	{
		public event Action<Collision2D> OnCollisionEntered;
		
		private void OnCollisionEnter2D(Collision2D other)
		{
			OnCollisionEntered?.Invoke(other);
		}
	}
}