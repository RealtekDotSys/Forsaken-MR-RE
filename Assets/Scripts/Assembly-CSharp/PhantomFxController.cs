public class PhantomFxController : global::UnityEngine.MonoBehaviour
{
	public enum States
	{
		Null = 0,
		Pause = 1,
		Manifest = 2,
		Walking = 3,
		Burning = 4,
		Overload = 5,
		OverloadExit = 6,
		Reposition = 7,
		Shutdown = 8,
		Jumpscare = 9,
		Haywire = 10
	}

	[global::UnityEngine.Header("Visual Effects")]
	public global::UnityEngine.GameObject manifestVfx;

	public global::UnityEngine.GameObject burnVfx;

	public global::UnityEngine.GameObject repositionVfx;

	public global::UnityEngine.GameObject shutdownVfx;

	public global::UnityEngine.GameObject burnDistortion;

	[global::UnityEngine.Header("Animatronic")]
	public global::UnityEngine.GameObject mesh;

	public global::UnityEngine.Animator animator;

	[global::UnityEngine.Header("VFX Duration")]
	public ParticleSystemDuration manifestVfxDuration;

	public ParticleSystemDuration repositionVfxDuration;

	public ParticleSystemDuration shutdownVfxDuration;

	[global::UnityEngine.SerializeField]
	[global::UnityEngine.Header("Debug Controls")]
	private bool editorMode;

	[global::UnityEngine.SerializeField]
	private PhantomFxController.States editorState;

	private PhantomFxController.States _state;

	private bool _burning;

	private bool _reposition;

	private float _targetWeight;

	private float _smoothTime;

	private float _animSpeed;

	private float _emissionRate;

	private global::UnityEngine.ParticleSystem _burnParticleSystem;

	private global::UnityEngine.ParticleSystem.EmissionModule _burnParticleEmission;

	private float _currentAnimLayerWeight;

	private float _animLayerBlendVelocity;

	private static readonly int HitReact;

	private static readonly int Shutdown;

	private static readonly int Jumpscare;

	private static readonly int Overload;

	private static readonly int OverloadEnd;

	private global::System.Action OnMeshToggled;

	private void add_OnMeshToggled(global::System.Action value)
	{
		OnMeshToggled = (global::System.Action)global::System.Delegate.Combine(OnMeshToggled, value);
	}

	private void remove_OnMeshToggled(global::System.Action value)
	{
		OnMeshToggled = (global::System.Action)global::System.Delegate.Remove(OnMeshToggled, value);
	}

	public void RegisterForMeshToggle(global::System.Action callback)
	{
		add_OnMeshToggled(callback);
	}

	public void UnregisterForMeshToggle(global::System.Action callback)
	{
		remove_OnMeshToggled(callback);
	}

	public float GetAnimationSpeedModifier()
	{
		return _animSpeed;
	}

	public void SetState(PhantomFxController.States newState)
	{
		_state = newState;
		ResetFx();
		switch (newState)
		{
		case PhantomFxController.States.Pause:
			_burning = false;
			_reposition = false;
			_targetWeight = 0f;
			_smoothTime = 0f;
			_animSpeed = 1f;
			_emissionRate = 0f;
			break;
		case PhantomFxController.States.Manifest:
			_burning = false;
			_reposition = false;
			_targetWeight = 0f;
			_smoothTime = 0f;
			_animSpeed = 1f;
			_emissionRate = 0f;
			break;
		case PhantomFxController.States.Walking:
			_burning = false;
			_reposition = false;
			_targetWeight = 0f;
			_smoothTime = 1f;
			_animSpeed = 1f;
			_emissionRate = 0f;
			break;
		case PhantomFxController.States.Burning:
			_burning = true;
			_reposition = false;
			_targetWeight = 1f;
			_smoothTime = 1f;
			_animSpeed = 0.5f;
			_emissionRate = 3f;
			break;
		case PhantomFxController.States.Overload:
			_burning = false;
			_reposition = false;
			_targetWeight = 0f;
			_smoothTime = 0f;
			_animSpeed = 1f;
			_emissionRate = 0f;
			break;
		case PhantomFxController.States.OverloadExit:
			_burning = false;
			_reposition = false;
			_targetWeight = 0f;
			_smoothTime = 0f;
			_animSpeed = 1f;
			_emissionRate = 0f;
			break;
		case PhantomFxController.States.Reposition:
			_burning = false;
			_reposition = true;
			_targetWeight = 0f;
			_smoothTime = 0f;
			_animSpeed = 1f;
			_emissionRate = 0f;
			break;
		case PhantomFxController.States.Shutdown:
			_burning = false;
			_reposition = false;
			_targetWeight = 0f;
			_smoothTime = 0f;
			_animSpeed = 1f;
			_emissionRate = 0f;
			break;
		case PhantomFxController.States.Jumpscare:
			_burning = false;
			_reposition = false;
			_targetWeight = 0f;
			_smoothTime = 0f;
			_animSpeed = 1f;
			_emissionRate = 0f;
			break;
		default:
			ResetFx();
			break;
		}
		ExecuteFx();
		ExecuteStateOnEnter();
	}

	private void ResetFx()
	{
		_burning = false;
		_reposition = false;
		_targetWeight = 0f;
		_smoothTime = 0f;
		_animSpeed = 1f;
		_emissionRate = 0f;
	}

	private void SetStateFxConfig()
	{
	}

	private void ExecuteStateOnEnter()
	{
		switch (_state)
		{
		case PhantomFxController.States.Pause:
			mesh.SetActive(value: false);
			manifestVfx.SetActive(value: false);
			repositionVfx.SetActive(value: false);
			shutdownVfx.SetActive(value: false);
			burnVfx.SetActive(value: false);
			break;
		case PhantomFxController.States.Manifest:
			animator.ResetTrigger("shutdown");
			animator.ResetTrigger("jumpscare");
			animator.gameObject.SetActive(value: false);
			mesh.SetActive(value: false);
			manifestVfx.SetActive(value: true);
			StartCoroutine(ToggleAnimator(status: true, 1f));
			StartCoroutine(ToggleMesh(status: true, 1f));
			StartCoroutine(TurnOffManifestVfx(2f));
			break;
		case PhantomFxController.States.Walking:
			animator.ResetTrigger("hitReact");
			break;
		case PhantomFxController.States.Burning:
			animator.SetTrigger("hitReact");
			break;
		case PhantomFxController.States.Overload:
			animator.ResetTrigger("haywire");
			animator.gameObject.SetActive(value: true);
			mesh.SetActive(value: true);
			animator.SetTrigger("overload");
			break;
		case PhantomFxController.States.OverloadExit:
			animator.gameObject.SetActive(value: true);
			mesh.SetActive(value: true);
			animator.SetTrigger("overloadEnd");
			break;
		case PhantomFxController.States.Reposition:
			animator.ResetTrigger("overload");
			animator.ResetTrigger("overloadEnd");
			StartCoroutine(ToggleMesh(status: false, 0.1f));
			StartCoroutine(ToggleAnimator(status: false, 0.1f));
			break;
		case PhantomFxController.States.Shutdown:
			animator.gameObject.SetActive(value: true);
			mesh.SetActive(value: true);
			animator.SetTrigger("shutdown");
			StartCoroutine(TurnOnShutdownVfx(3f));
			StartCoroutine(ToggleMesh(status: false, 3f));
			break;
		case PhantomFxController.States.Jumpscare:
			animator.ResetTrigger("haywire");
			animator.SetTrigger("jumpscare");
			mesh.SetActive(value: true);
			animator.gameObject.SetActive(value: true);
			break;
		case PhantomFxController.States.Haywire:
			animator.SetTrigger("haywire");
			animator.gameObject.SetActive(value: true);
			mesh.SetActive(value: true);
			break;
		default:
			animator.gameObject.SetActive(value: true);
			mesh.SetActive(value: true);
			shutdownVfx.SetActive(value: false);
			break;
		}
	}

	private void ExecuteFx()
	{
		burnDistortion.SetActive(_burning);
		burnVfx.SetActive(_burning);
		repositionVfx.SetActive(_reposition);
		animator.speed = _animSpeed;
		_burnParticleEmission.rateOverTime = _emissionRate;
	}

	private void Start()
	{
		editorMode = false;
		editorState = PhantomFxController.States.Pause;
		_burnParticleSystem = burnVfx.GetComponent<global::UnityEngine.ParticleSystem>();
		_burnParticleEmission = _burnParticleSystem.emission;
		_state = PhantomFxController.States.Pause;
		ResetFx();
		ExecuteFx();
		ExecuteStateOnEnter();
	}

	private void Update()
	{
		if (editorMode && _state != editorState)
		{
			SetState(editorState);
		}
		animator.SetLayerWeight(1, GetLayerBlendWeight(_targetWeight, _smoothTime));
	}

	private global::System.Collections.IEnumerator TurnOffManifestVfx(float seconds)
	{
		yield return new global::UnityEngine.WaitForSeconds(seconds);
		manifestVfx.SetActive(value: false);
		yield return null;
	}

	private global::System.Collections.IEnumerator TurnOnShutdownVfx(float seconds)
	{
		yield return new global::UnityEngine.WaitForSeconds(seconds);
		shutdownVfx.SetActive(value: true);
		_burnParticleEmission.rateOverTime = _emissionRate;
		yield return null;
	}

	private global::System.Collections.IEnumerator ToggleAnimator(bool status, float seconds)
	{
		global::UnityEngine.Debug.Log("toggled animator for status " + status + " and seconds " + seconds);
		yield return new global::UnityEngine.WaitForSeconds(seconds);
		animator.gameObject.SetActive(status);
		yield return null;
	}

	private global::System.Collections.IEnumerator ToggleMesh(bool status, float seconds)
	{
		yield return new global::UnityEngine.WaitForSeconds(seconds);
		mesh.SetActive(status);
		if (OnMeshToggled != null)
		{
			OnMeshToggled();
		}
		_burnParticleEmission.rateOverTime = _emissionRate;
	}

	private float GetLayerBlendWeight(float targetWeight, float smoothTime)
	{
		return global::UnityEngine.Mathf.SmoothDamp(animator.GetLayerWeight(1), targetWeight, ref _animLayerBlendVelocity, smoothTime);
	}

	static PhantomFxController()
	{
		HitReact = global::UnityEngine.Animator.StringToHash("hitReact");
		Shutdown = global::UnityEngine.Animator.StringToHash("shutdown");
		Jumpscare = global::UnityEngine.Animator.StringToHash("jumpscare");
		Overload = global::UnityEngine.Animator.StringToHash("overload");
		OverloadEnd = global::UnityEngine.Animator.StringToHash("overloadEnd");
		HitReact = global::UnityEngine.Animator.StringToHash("hitReact");
		Shutdown = global::UnityEngine.Animator.StringToHash("shutdown");
		Jumpscare = global::UnityEngine.Animator.StringToHash("jumpscare");
		Overload = global::UnityEngine.Animator.StringToHash("overload");
		OverloadEnd = global::UnityEngine.Animator.StringToHash("overloadEnd");
	}
}
