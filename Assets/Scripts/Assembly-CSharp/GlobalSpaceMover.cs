public class GlobalSpaceMover
{
	private global::UnityEngine.Transform _cameraTransform;

	private global::UnityEngine.Transform _animatronicPrefabTransform;

	private float _movementUnitsPerSecond;

	public float GetDistanceFromCamera()
	{
		return (_animatronicPrefabTransform.position - _cameraTransform.position).magnitude;
	}

	private float GetFlatDistanceFromCamera()
	{
		global::UnityEngine.Vector3 vector = _cameraTransform.position - _animatronicPrefabTransform.position;
		return new global::UnityEngine.Vector3(vector.x, 0f, vector.z).magnitude;
	}

	public void RotateAroundCamera()
	{
		global::UnityEngine.Vector3 vector = _animatronicPrefabTransform.position - _cameraTransform.position;
		float angle = _movementUnitsPerSecond * global::UnityEngine.Time.deltaTime;
		global::UnityEngine.Vector3 vector2 = new global::UnityEngine.Vector3
		{
			x = vector.x,
			y = 0f,
			z = vector.z
		};
		global::UnityEngine.Vector3 vector3 = global::UnityEngine.Quaternion.AngleAxis(angle, global::UnityEngine.Vector3.up) * vector2;
		_animatronicPrefabTransform.forward = vector3 - vector2;
		_animatronicPrefabTransform.position = _cameraTransform.position + vector3;
	}

	public bool MoveTowardCamera()
	{
		global::UnityEngine.Vector3 vector = _cameraTransform.position - _animatronicPrefabTransform.position;
		global::UnityEngine.Vector3 vector2 = new global::UnityEngine.Vector3(vector.x, 0f, vector.z);
		float num = _movementUnitsPerSecond * global::UnityEngine.Time.deltaTime;
		if (num < GetFlatDistanceFromCamera())
		{
			global::UnityEngine.Vector3 translation = vector2.normalized * num;
			_animatronicPrefabTransform.Translate(translation, global::UnityEngine.Space.World);
			_animatronicPrefabTransform.forward = vector2.normalized;
			return false;
		}
		return true;
	}

	public void SetMovementUnitsPerSecond(float movementUnitsPerSecond)
	{
		_movementUnitsPerSecond = movementUnitsPerSecond;
	}

	public GlobalSpaceMover(global::UnityEngine.Transform animatronicPrefabTransform, global::UnityEngine.Transform cameraTransform)
	{
		_cameraTransform = cameraTransform;
		_animatronicPrefabTransform = animatronicPrefabTransform;
	}

	public void Teardown()
	{
		_cameraTransform = null;
		_animatronicPrefabTransform = null;
	}
}
