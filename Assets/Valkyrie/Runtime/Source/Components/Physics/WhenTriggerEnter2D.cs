using System;
using UnityEngine;

namespace Valkyrie.Components.Physics
{
	public class WhenTriggerEnter2D : MonoBehaviour
	{
		public event Action<Collider2D> OnTriggerEntered;
		
		private void OnTriggerEnter2D(Collider2D other)
		{
			OnTriggerEntered?.Invoke(other);
		}
	}
}