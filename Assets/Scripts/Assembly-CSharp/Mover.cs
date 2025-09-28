public class Mover
{
	private global::UnityEngine.Transform _cameraStableTransform;

	private global::UnityEngine.Transform _animatronicPrefabTransform;

	private global::UnityEngine.Transform _rootBone;

	private global::UnityEngine.Transform _transformBone;

	private AnimatorDispatcher _animatorDispatcher;

	private SpaceMover _spaceMover;

	private readonly TransformOverrider _transformOverrider;

	private bool _isWalking;

	private float _workshopRotationSpeed;

	public global::UnityEngine.Vector3 GetRootBonePosition()
	{
		return _rootBone.position;
	}

	public global::UnityEngine.Vector3 GetPosition()
	{
		return _animatronicPrefabTransform.localPosition;
	}

	public global::UnityEngine.Vector3 GetForward()
	{
		return _animatronicPrefabTransform.forward;
	}

	public void SetMovementMode(SpaceMover.Mode mode)
	{
		switch (mode)
		{
		case SpaceMover.Mode.LOCAL:
			SetTransformOverrideMode(TransformOverrider.Mode.AttackLocal);
			break;
		case SpaceMover.Mode.GLOBAL:
			SetTransformOverrideMode(TransformOverrider.Mode.AttackGlobal);
			break;
		case SpaceMover.Mode.SCAVENGING:
			SetTransformOverrideMode(TransformOverrider.Mode.Scavenging);
			break;
		default:
			SetTransformOverrideMode(TransformOverrider.Mode.AttackLocal);
			break;
		}
		_spaceMover.SetMoveTowardsCameraMode(mode);
	}

	public void SetTransformOverrideMode(TransformOverrider.Mode mode)
	{
		_transformOverrider.SetOverrideMode(mode);
	}

	public float GetAbsoluteAngleBetweenPositionAndCamera(global::UnityEngine.Vector3 position)
	{
		return global::System.Math.Abs(GetSignedAngleBetweenPositionAndCamera(position));
	}

	public float GetAbsoluteAngleFromCamera()
	{
		return GetAbsoluteAngleBetweenPositionAndCamera(GetRootBonePosition());
	}

	public float GetSignedAngleBetweenPositionAndCamera(global::UnityEngine.Vector3 position)
	{
		return global::UnityEngine.Vector3.SignedAngle(position, _cameraStableTransform.forward, global::UnityEngine.Vector3.up);
	}

	public float GetSignedAngleFromCamera()
	{
		return GetSignedAngleBetweenPositionAndCamera(GetRootBonePosition());
	}

	public float GetDistanceFromCamera()
	{
		return _spaceMover.GetDistanceFromCamera();
	}

	public void Teleport(float angleFromCamera, float distanceFromCamera, bool faceCamera)
	{
		if (_cameraStableTransform == null)
		{
			global::UnityEngine.Debug.LogError("Animatronic3D Mover Teleport - Camera transform is null. Cannot teleport");
			return;
		}
		global::UnityEngine.Vector3 vector = global::UnityEngine.Vector3.ProjectOnPlane(_cameraStableTransform.forward, global::UnityEngine.Vector3.up);
		global::UnityEngine.Vector3 vector2 = global::UnityEngine.Quaternion.AngleAxis(angleFromCamera, global::UnityEngine.Vector3.up) * vector * ((global::System.Math.Abs(distanceFromCamera) == 0f) ? 0.001f : distanceFromCamera);
		_animatronicPrefabTransform.position = _cameraStableTransform.position + vector2;
		global::UnityEngine.Debug.Log("DISTANCE IS - " + GetDistanceFromCamera());
		if (faceCamera)
		{
			global::UnityEngine.Vector3 vector3 = _cameraStableTransform.position - _animatronicPrefabTransform.position;
			_animatronicPrefabTransform.forward = global::UnityEngine.Vector3.ProjectOnPlane(vector3, global::UnityEngine.Vector3.up);
		}
		VerifyAnimatronicPrefabUpVectorMatchesWorld("Teleport");
	}

	public void TeleportToLocalPosition(global::UnityEngine.Vector3 position, global::UnityEngine.Vector3 forward)
	{
		_animatronicPrefabTransform.localPosition = position;
		_animatronicPrefabTransform.forward = forward;
		VerifyAnimatronicPrefabUpVectorMatchesWorld("TeleportToLocalPosition");
	}

	public void TeleportInFrontOfCamera(float distanceFromCamera)
	{
		if (_cameraStableTransform == null)
		{
			global::UnityEngine.Debug.LogError("Animatronic3D Teleport - Camera transform is null. Cannot teleport");
			return;
		}
		Teleport(0f, distanceFromCamera, faceCamera: true);
		VerifyAnimatronicPrefabUpVectorMatchesWorld("TeleportInFrontOfCamera");
	}

	public void TeleportAtCurrentAngle(float distanceFromCamera)
	{
		if (_cameraStableTransform == null)
		{
			global::UnityEngine.Debug.LogError("Animatronic3D Teleport - Camera transform is null. Cannot teleport");
			return;
		}
		global::UnityEngine.Vector3 normalized = (_animatronicPrefabTransform.position - _cameraStableTransform.position).normalized;
		_animatronicPrefabTransform.position = _cameraStableTransform.position + normalized * distanceFromCamera;
		_animatronicPrefabTransform.forward = (_cameraStableTransform.position - _animatronicPrefabTransform.position).normalized;
		VerifyAnimatronicPrefabUpVectorMatchesWorld("TeleportAtCurrentAngle");
	}

	public void ScavengingTeleportToStartPoint(ScavengingEnvironment env)
	{
		_spaceMover.ScavengingTeleportToStartPoint(env);
	}

	public void SwapWaypointIncrement()
	{
		_spaceMover.SwapWaypointIncrement();
	}

	public void InvertFacing()
	{
		_animatronicPrefabTransform.forward = -_animatronicPrefabTransform.forward;
	}

	public void RotateFacingInWorkshop(global::UnityEngine.Vector3 mPrevPos, global::UnityEngine.Vector3 mCurrPos)
	{
		_transformBone.Rotate(0f, 0f - (mCurrPos.x - mPrevPos.x) * _workshopRotationSpeed, 0f, global::UnityEngine.Space.Self);
	}

	public void ResetFacing()
	{
		_transformBone.localEulerAngles = new global::UnityEngine.Vector3(_transformBone.localRotation.x, 0f, _transformBone.localRotation.z);
	}

	public bool IsMoving()
	{
		return _spaceMover.IsMoving;
	}

	public void StopMovement()
	{
		_spaceMover.StopMovement();
	}

	public float GetMoveSpeed()
	{
		return _spaceMover.GetMoveSpeed();
	}

	public void MoveInCircleAroundCamera(float degreesPerSecond, bool isWalking, float duration = -1f)
	{
		_isWalking = isWalking;
		_spaceMover.MoveInCircleAroundOrigin(degreesPerSecond, duration);
	}

	public void MoveInLineTowardCamera(float unitsPerSecond, bool isWalking, float duration = -1f)
	{
		_isWalking = isWalking;
		_spaceMover.MoveInLineTowardOrigin(unitsPerSecond, duration);
	}

	public void MoveFollowWaypoints(float unitsPerSecond, bool isWalking, float duration = -1f)
	{
		_isWalking = isWalking;
		_spaceMover.MoveFollowWaypoints(unitsPerSecond, duration);
	}

	public void Update()
	{
		_spaceMover.Update();
		VerifyAnimatronicPrefabUpVectorMatchesWorld("Update");
	}

	public void LateUpdate()
	{
		_transformOverrider.LateUpdate();
		VerifyAnimatronicPrefabUpVectorMatchesWorld("LateUpdate");
	}

	private void VerifyAnimatronicPrefabUpVectorMatchesWorld(string callingFunction)
	{
		if (!_transformOverrider.IsFollowingCameraPosition())
		{
			float num = global::UnityEngine.Vector3.Dot(_animatronicPrefabTransform.up, global::UnityEngine.Vector3.up);
			if (!(1f - num <= 0.0001f))
			{
				global::UnityEngine.Debug.LogError("Mover VerifyAnimatronicPrefabUpVectorMatchesWorld - The animatronic is not upright.");
			}
		}
	}

	private void StartedMoving()
	{
		_animatorDispatcher.SetAnimationBool(AnimationBool.IsWalking, _isWalking);
	}

	private void StoppedMoving()
	{
		_animatorDispatcher.SetAnimationBool(AnimationBool.IsWalking, value: false);
	}

	public Mover(global::UnityEngine.Transform cameraStableTransform, global::UnityEngine.Transform animatronic3DTransform, AnimatorDispatcher animatorDispatcher, AnimatronicModelConfig animatronicModelConfig, CloakController cloakController)
	{
		_cameraStableTransform = cameraStableTransform;
		_animatronicPrefabTransform = animatronicModelConfig.ModelTransforms.PrefabTransform;
		_rootBone = animatronicModelConfig.ModelTransforms.RootBone;
		_transformBone = animatronicModelConfig.ModelTransforms.TransformBone;
		_animatorDispatcher = animatorDispatcher;
		_spaceMover = new SpaceMover(_animatronicPrefabTransform, _cameraStableTransform);
		_transformOverrider = new TransformOverrider(_cameraStableTransform, animatronic3DTransform, animatronicModelConfig.ModelTransforms, animatronicModelConfig.AdditionalOffsets, cloakController, animatronicModelConfig.AABBCollider, animatronicModelConfig.AnimatronicMaterialController);
		float num = (float)global::UnityEngine.Screen.width * 0.5f;
		_workshopRotationSpeed = 180f / num;
		_spaceMover.add_OnStartedMoving(StartedMoving);
		_spaceMover.add_OnStoppedMoving(StoppedMoving);
	}

	public void Teardown()
	{
		_spaceMover.remove_OnStartedMoving(StartedMoving);
		_spaceMover.remove_OnStoppedMoving(StoppedMoving);
		_transformOverrider.Teardown();
		_spaceMover.Teardown();
		_rootBone = null;
		_animatorDispatcher = null;
		_cameraStableTransform = null;
	}
}
