namespace SRF
{
	public abstract class SRMonoBehaviour : global::UnityEngine.MonoBehaviour
	{
		private global::UnityEngine.Collider _collider;

		private global::UnityEngine.Transform _transform;

		private global::UnityEngine.Rigidbody _rigidBody;

		private global::UnityEngine.GameObject _gameObject;

		private global::UnityEngine.Rigidbody2D _rigidbody2D;

		private global::UnityEngine.Collider2D _collider2D;

		public global::UnityEngine.Transform CachedTransform
		{
			[global::System.Diagnostics.DebuggerStepThrough]
			[global::System.Diagnostics.DebuggerNonUserCode]
			get
			{
				if (_transform == null)
				{
					_transform = base.transform;
				}
				return _transform;
			}
		}

		public global::UnityEngine.Collider CachedCollider
		{
			[global::System.Diagnostics.DebuggerStepThrough]
			[global::System.Diagnostics.DebuggerNonUserCode]
			get
			{
				if (_collider == null)
				{
					_collider = GetComponent<global::UnityEngine.Collider>();
				}
				return _collider;
			}
		}

		public global::UnityEngine.Collider2D CachedCollider2D
		{
			[global::System.Diagnostics.DebuggerStepThrough]
			[global::System.Diagnostics.DebuggerNonUserCode]
			get
			{
				if (_collider2D == null)
				{
					_collider2D = GetComponent<global::UnityEngine.Collider2D>();
				}
				return _collider2D;
			}
		}

		public global::UnityEngine.Rigidbody CachedRigidBody
		{
			[global::System.Diagnostics.DebuggerStepThrough]
			[global::System.Diagnostics.DebuggerNonUserCode]
			get
			{
				if (_rigidBody == null)
				{
					_rigidBody = GetComponent<global::UnityEngine.Rigidbody>();
				}
				return _rigidBody;
			}
		}

		public global::UnityEngine.Rigidbody2D CachedRigidBody2D
		{
			[global::System.Diagnostics.DebuggerStepThrough]
			[global::System.Diagnostics.DebuggerNonUserCode]
			get
			{
				if (_rigidbody2D == null)
				{
					_rigidbody2D = GetComponent<global::UnityEngine.Rigidbody2D>();
				}
				return _rigidbody2D;
			}
		}

		public global::UnityEngine.GameObject CachedGameObject
		{
			[global::System.Diagnostics.DebuggerStepThrough]
			[global::System.Diagnostics.DebuggerNonUserCode]
			get
			{
				if (_gameObject == null)
				{
					_gameObject = base.gameObject;
				}
				return _gameObject;
			}
		}

		public new global::UnityEngine.Transform transform => CachedTransform;

		public global::UnityEngine.Collider collider => CachedCollider;

		public global::UnityEngine.Collider2D collider2D => CachedCollider2D;

		public global::UnityEngine.Rigidbody rigidbody => CachedRigidBody;

		public global::UnityEngine.Rigidbody2D rigidbody2D => CachedRigidBody2D;

		public new global::UnityEngine.GameObject gameObject => CachedGameObject;

		[global::System.Diagnostics.DebuggerNonUserCode]
		[global::System.Diagnostics.DebuggerStepThrough]
		protected void AssertNotNull(object value, string fieldName = null)
		{
			SRDebugUtil.AssertNotNull(value, fieldName, this);
		}

		[global::System.Diagnostics.DebuggerNonUserCode]
		[global::System.Diagnostics.DebuggerStepThrough]
		protected void Assert(bool condition, string message = null)
		{
			SRDebugUtil.Assert(condition, message, this);
		}

		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		[global::System.Diagnostics.DebuggerNonUserCode]
		[global::System.Diagnostics.DebuggerStepThrough]
		protected void EditorAssertNotNull(object value, string fieldName = null)
		{
			AssertNotNull(value, fieldName);
		}

		[global::System.Diagnostics.Conditional("UNITY_EDITOR")]
		[global::System.Diagnostics.DebuggerNonUserCode]
		[global::System.Diagnostics.DebuggerStepThrough]
		protected void EditorAssert(bool condition, string message = null)
		{
			Assert(condition, message);
		}
	}
}
