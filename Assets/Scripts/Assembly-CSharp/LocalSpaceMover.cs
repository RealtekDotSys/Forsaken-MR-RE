public class LocalSpaceMover
{
	private global::UnityEngine.Transform _animatronicPrefabTransform;

	private float _movementUnitsPerSecond;

	private bool _rotateAroundOrigin;

	public float GetDistanceFromLocalOrigin()
	{
		return _animatronicPrefabTransform.localPosition.magnitude;
	}

	public void SetMovementUnitsPerSecond(float movementSpeed)
	{
		_movementUnitsPerSecond = movementSpeed;
	}

	public void RotateAroundOrigin()
	{
		global::UnityEngine.Vector3 vector = global::UnityEngine.Quaternion.AngleAxis(_movementUnitsPerSecond * global::UnityEngine.Time.deltaTime, global::UnityEngine.Vector3.up) * _animatronicPrefabTransform.localPosition;
		_animatronicPrefabTransform.forward = vector - _animatronicPrefabTransform.localPosition;
		_animatronicPrefabTransform.localPosition = vector;
	}

	public bool MoveTowardOrigin()
	{
		float num = _movementUnitsPerSecond * global::UnityEngine.Time.deltaTime;
		if (num < GetDistanceFromLocalOrigin())
		{
			global::UnityEngine.Vector3 translation = (-_animatronicPrefabTransform.localPosition).normalized * num;
			_animatronicPrefabTransform.Translate(translation, global::UnityEngine.Space.World);
			_animatronicPrefabTransform.forward = -_animatronicPrefabTransform.localPosition;
			return false;
		}
		_animatronicPrefabTransform.localPosition = global::UnityEngine.Vector3.zero;
		return true;
	}

	public LocalSpaceMover(global::UnityEngine.Transform animatronicPrefabTransform)
	{
		_animatronicPrefabTransform = animatronicPrefabTransform;
	}

	public void Teardown()
	{
		_animatronicPrefabTransform = null;
	}
}
