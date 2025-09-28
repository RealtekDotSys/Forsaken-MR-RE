namespace Animatronics.Animatronic3d.Model
{
	public class OrtonsLittleOverrider : global::UnityEngine.MonoBehaviour
	{
		[global::UnityEngine.Header("Set by script")]
		public AnimatronicModelConfig modelConfig;

		public AnimatronicMaterialController matController;

		[global::UnityEngine.Header("Activators")]
		public bool AttackOffset;

		public bool WorkshopOffset;

		public bool NoOffset;

		private bool JumpscareOffset;

		private bool ShouldCheckForJumpscareEnd;

		private CloakSettings _settings;

		private float _distance;

		private global::UnityEngine.Vector3 _direction;

		private CloakGroup _bodyCloak;

		private CloakGroup _eyeCloak;

		private ModelTransforms _transforms;

		private AdditionalOffsets _offsets;

		private global::UnityEngine.Vector3 _additionalOffset;

		private global::UnityEngine.Vector3 _additionalRotation;

		[global::UnityEngine.Header("DO NOT CHANGE.")]
		public bool Charging;

		private void Start()
		{
			modelConfig = GetComponent<AnimatronicModelConfig>();
			matController = modelConfig.AnimatronicMaterialController;
			_transforms = modelConfig.ModelTransforms;
			_offsets = modelConfig.AdditionalOffsets;
			_settings = modelConfig.CloakSettings;
			_bodyCloak = new CloakGroup();
			_eyeCloak = new CloakGroup();
			_bodyCloak.Percent = 0f;
			_bodyCloak.BeginTime = -1f;
			_eyeCloak.Percent = 0f;
			_eyeCloak.BeginTime = -1f;
			CalculateDirectionAndDistance();
		}

		private void LateUpdate()
		{
			if (ShouldCheckForJumpscareEnd && !IsAnimationTagActive("Jumpscare"))
			{
				JumpscareOffset = false;
				ShouldCheckForJumpscareEnd = false;
				modelConfig.Animator.ResetTrigger("Jumpscare");
				modelConfig.Animator.SetInteger("AnimationMode", 0);
				modelConfig.Animator.SetTrigger("SwitchMode");
			}
			if (_offsets != null)
			{
				if (!JumpscareOffset)
				{
					if (AttackOffset)
					{
						AttackOffset = false;
						_additionalOffset = _offsets.AttackOffset;
						_additionalRotation = _offsets.AttackRotation;
					}
					if (WorkshopOffset)
					{
						WorkshopOffset = false;
						_additionalOffset = _offsets.WorkshopOffset;
						_additionalRotation = _offsets.WorkshopRotation;
					}
					if (NoOffset)
					{
						NoOffset = false;
						_additionalOffset = global::UnityEngine.Vector3.zero;
						_additionalRotation = global::UnityEngine.Vector3.zero;
					}
				}
				else
				{
					_additionalOffset = _offsets.JumpscareOffset;
					_additionalRotation = _offsets.JumpscareRotation;
				}
				SetAdditionalOffset();
				OverrideAnimatedCloakPlane();
				OverrideAnimatedEyeCloakPlane();
			}
			matController.LaterUpdater();
		}

		public void SetCloakState(bool cloakEnabled)
		{
			_bodyCloak.BeginTime = -1f;
			_bodyCloak.Percent = ((!cloakEnabled) ? 0f : 1f);
		}

		public void BeginCloak()
		{
			global::UnityEngine.Debug.Log("BeginCloak called!");
			_bodyCloak.BeginTime = global::UnityEngine.Time.time;
			global::UnityEngine.Debug.Log(_bodyCloak.BeginTime);
			_bodyCloak.IsCloaking = true;
			if (_settings.ShouldUpdateEveryTime)
			{
				CalculateDirectionAndDistance();
			}
		}

		public void BeginDecloak()
		{
			_bodyCloak.BeginTime = global::UnityEngine.Time.time;
			_bodyCloak.IsCloaking = false;
			if (_settings.ShouldUpdateEveryTime)
			{
				CalculateDirectionAndDistance();
			}
		}

		public global::UnityEngine.Vector3 GetCloakPlanePosition()
		{
			UpdateCloakPercent();
			return CalculateCloakPlanePosition();
		}

		public void SetEyeCloakState(bool eyeCloakEnabled)
		{
			_eyeCloak.BeginTime = -1f;
			_eyeCloak.Percent = ((!eyeCloakEnabled) ? 0f : 1f);
		}

		public void BeginEyeCloak()
		{
			_eyeCloak.BeginTime = global::UnityEngine.Time.time;
			_eyeCloak.IsCloaking = true;
			if (_settings.ShouldUpdateEveryTime)
			{
				CalculateDirectionAndDistance();
			}
		}

		public void BeginEyeDecloak()
		{
			_eyeCloak.BeginTime = global::UnityEngine.Time.time;
			_eyeCloak.IsCloaking = false;
			if (_settings.ShouldUpdateEveryTime)
			{
				CalculateDirectionAndDistance();
			}
		}

		public global::UnityEngine.Vector3 GetEyeCloakPlanePosition()
		{
			return CalculateEyeCloakPlanePosition();
		}

		private void CalculateDirectionAndDistance()
		{
			global::UnityEngine.Vector3 vector = _settings.CloakedRevealPlanePosition - _settings.DecloakedRevealPlanePosition;
			_distance = vector.magnitude;
			_direction = vector / _distance;
		}

		private void UpdateCloakPercent()
		{
			if (_settings.ShouldHideEyes)
			{
				BeginEyeCloak();
				_settings.ShouldHideEyes = false;
			}
			if (_settings.ShouldShowEyes)
			{
				BeginEyeDecloak();
				_settings.ShouldShowEyes = false;
			}
			if (_settings.ShouldCloak)
			{
				BeginCloak();
				_settings.ShouldCloak = false;
			}
			if (_settings.ShouldDecloak)
			{
				BeginDecloak();
				_settings.ShouldDecloak = false;
			}
			UpdateBodyCloak();
			UpdateEyeCloak();
		}

		private void UpdateBodyCloak()
		{
			if (_bodyCloak.BeginTime < 0f)
			{
				return;
			}
			float num = ((!_bodyCloak.IsCloaking) ? _settings.DecloakTime : _settings.CloakTime);
			if (num == 0f)
			{
				global::UnityEngine.Debug.Log("Cloak/Decloak time is fucking 0 INSTANT");
				CompleteCloakOrDecloak(_bodyCloak);
				return;
			}
			float num2 = global::UnityEngine.Time.time - _bodyCloak.BeginTime;
			_bodyCloak.Percent = num2 / num;
			global::UnityEngine.Mathf.Clamp(_bodyCloak.Percent, 0f, 1f);
			if (_bodyCloak.Percent >= 1f)
			{
				global::UnityEngine.Debug.Log("Fully cloaked/decloaked boss!");
				CompleteCloakOrDecloak(_bodyCloak);
			}
			else if (_bodyCloak.IsCloaking)
			{
				global::UnityEngine.Debug.Log("Just cloakin around!");
			}
			else
			{
				global::UnityEngine.Debug.Log("Just decloakin around!");
				_bodyCloak.Percent = 1f - _bodyCloak.Percent;
			}
		}

		private void UpdateEyeCloak()
		{
			if (_eyeCloak.BeginTime < 0f)
			{
				return;
			}
			if (_eyeCloak == null)
			{
				global::UnityEngine.Debug.Log("Eye cloak is null.");
				_eyeCloak = new CloakGroup();
				_eyeCloak.Percent = 0f;
				return;
			}
			float num = ((!_eyeCloak.IsCloaking) ? _settings.DecloakTime : _settings.CloakTime);
			if (num == 0f)
			{
				global::UnityEngine.Debug.Log("Cloak/Decloak time is fucking 0 INSTANT");
			}
			else
			{
				float num2 = global::UnityEngine.Time.time - _eyeCloak.BeginTime;
				_eyeCloak.Percent = num2 / num;
				global::UnityEngine.Mathf.Clamp(_eyeCloak.Percent, 0f, 1f);
				if (!(_eyeCloak.Percent >= 1f))
				{
					if (!_eyeCloak.IsCloaking)
					{
						_eyeCloak.Percent = 1f - _eyeCloak.Percent;
					}
					return;
				}
			}
			CompleteCloakOrDecloak(_eyeCloak);
		}

		private static void CompleteCloakOrDecloak(CloakGroup cloakGroup)
		{
			global::UnityEngine.Debug.Log("Complete is called.");
			if (cloakGroup != null)
			{
				cloakGroup.BeginTime = -1f;
				cloakGroup.Percent = (cloakGroup.IsCloaking ? 1f : 0f);
			}
		}

		private global::UnityEngine.Vector3 CalculateEyeCloakPlanePosition()
		{
			global::UnityEngine.Vector3 vector = _direction * _distance * _eyeCloak.Percent;
			return _settings.DecloakedRevealPlanePosition + vector;
		}

		private global::UnityEngine.Vector3 CalculateCloakPlanePosition()
		{
			global::UnityEngine.Vector3 vector = _direction * _distance * _bodyCloak.Percent;
			return _settings.DecloakedRevealPlanePosition + vector;
		}

		private void OverrideAnimatedCloakPlane()
		{
			_transforms.CloakPlaneBone.localPosition = GetCloakPlanePosition();
		}

		private void OverrideAnimatedEyeCloakPlane()
		{
			_transforms.EyeCloakPlane.localPosition = GetEyeCloakPlanePosition();
		}

		private void SetAdditionalOffset()
		{
			if (!Charging)
			{
				_transforms.ModelRoot.localPosition = _additionalOffset;
				_transforms.ModelRoot.localEulerAngles = _additionalRotation;
				if (_additionalOffset == _offsets.AttackOffset)
				{
					_transforms.TransformBone.localPosition = new global::UnityEngine.Vector3
					{
						x = global::UnityEngine.Vector3.zero.x,
						y = global::UnityEngine.Vector3.zero.y + -0.5f,
						z = global::UnityEngine.Vector3.zero.z
					};
				}
			}
			else
			{
				_transforms.ModelRoot.localPosition = _offsets.AttackOffset;
				_transforms.ModelRoot.localEulerAngles = _offsets.AttackRotation;
				_transforms.TransformBone.localPosition = new global::UnityEngine.Vector3
				{
					x = global::UnityEngine.Vector3.zero.x,
					y = global::UnityEngine.Vector3.zero.y + -0.5f,
					z = global::UnityEngine.Vector3.zero.z
				};
			}
		}

		public void Jumpscare()
		{
			modelConfig.Animator.SetTrigger("Jumpscare");
			Charging = false;
			ShouldCheckForJumpscareEnd = true;
		}

		private bool IsAnimationTagActive(string animationTag)
		{
			return modelConfig.Animator.GetCurrentAnimatorStateInfo(0).IsTag(animationTag);
		}
	}
}
