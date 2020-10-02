using UnityEngine;

namespace Valkyrie 
{
	public class BaseBehaviour : MonoBehaviour
	{
		private Animator _animator;
		private AudioSource _audio;
		private Camera _camera;
		private Collider _collider;
		private Collider2D _collider2D;
		private GameObject _gameObject;
		private Light _light;
		private ParticleSystem _particleSystem;
		private Renderer _renderer;
		private Rigidbody _rigidbody;
		private Rigidbody2D _rigidbody2D;
		private Transform _transform;
		private RectTransform _rectTransform;

		protected virtual bool LocateComponentsInChildren => false;

		public Animator Animator => _animator != null ? _animator : _animator = LocateComponent<Animator>();
		public RectTransform RectTransform => _rectTransform != null ? _rectTransform : _rectTransform = LocateComponent<RectTransform>();
		public AudioSource Audio => _audio != null ? _audio : _audio = LocateComponent<AudioSource>();
		public Camera Camera => _camera != null ? _camera : _camera = LocateComponent<Camera>();
		public Collider Collider => _collider != null ? _collider : _collider = LocateComponent<Collider>();
		public Collider2D Collider2D => _collider2D != null ? _collider2D : _collider2D = LocateComponent<Collider2D>();
		public GameObject GameObject => _gameObject != null ? _gameObject : _gameObject = gameObject;
		public Light Light => _light != null ? _light : _light = LocateComponent<Light>();
		public ParticleSystem ParticleSystem => _particleSystem != null ? _particleSystem : _particleSystem = LocateComponent<ParticleSystem>();
		public Renderer Renderer => _renderer != null ? _renderer : _renderer = LocateComponent<Renderer>();
		public Rigidbody Rigidbody => _rigidbody != null ? _rigidbody : _rigidbody = LocateComponent<Rigidbody>();
		public Rigidbody2D Rigidbody2D => _rigidbody2D != null ? _rigidbody2D : _rigidbody2D = LocateComponent<Rigidbody2D>();
		public Transform Transform => _transform != null ? _transform : _transform = transform;

		private T LocateComponent<T>() where T : Component 
		{
			var component = GetComponent<T>();
			if (component == null && LocateComponentsInChildren) 
				component = GetComponentInChildren<T>();
			
			return component;
		}
	}
}

