public class Minireena : global::UnityEngine.MonoBehaviour
{
	public enum Orientation
	{
		Left = 0,
		Right = 1
	}

	public bool editorMode;

	public global::UnityEngine.Camera targetCamera;

	public Minireena.Orientation orientation;

	public float verticalOffset;

	public float horizontalOffset;

	public float distanceOffset;

	public float timeTillJumpscare;

	private global::UnityEngine.Vector3 _worldPoint;

	private global::UnityEngine.Animator _animator;

	private static readonly int ClimbId = global::UnityEngine.Animator.StringToHash("Climb");

	private static readonly int FallId = global::UnityEngine.Animator.StringToHash("Fall");

	private static readonly int JumpscareId = global::UnityEngine.Animator.StringToHash("Jumpscare");

	private static readonly int HideId = global::UnityEngine.Animator.StringToHash("Hide");

	private const string JumpscareCoroutine = "TimeTillJumpscare";

	private bool _initialized;

	private float _climbSpeed;

	private bool _isHidden;

	private int _pixelWidth;

	private int _pixelHeight;

	[global::UnityEngine.Header("Debug Toggles")]
	public bool DoClimb;

	public bool DoFall;

	public float SpawnTime { get; set; }

	public float ShakeTolerance { get; set; }

	public float CurrentToleranceModifier { get; set; }

	public float PreviousToleranceModifier { get; set; }

	private void Init()
	{
		_pixelWidth = targetCamera.pixelWidth;
		_pixelHeight = targetCamera.pixelHeight;
		MoveToEdge();
		_animator = GetComponent<global::UnityEngine.Animator>();
		_initialized = true;
	}

	private void Update()
	{
		if (editorMode)
		{
			MoveToEdge();
		}
		if (DoClimb)
		{
			DoClimb = false;
			Climb();
		}
		if (DoFall)
		{
			DoFall = false;
			Fall();
		}
	}

	public void Climb()
	{
		if (!_initialized)
		{
			Init();
		}
		base.gameObject.SetActive(value: false);
		MoveToEdge();
		base.gameObject.SetActive(value: true);
		_animator.speed = _climbSpeed;
		_animator.ResetTrigger(FallId);
		_animator.ResetTrigger(ClimbId);
		_animator.ResetTrigger(JumpscareId);
		_animator.SetTrigger(ClimbId);
		_isHidden = false;
		base.gameObject.SetActive(value: true);
		SpawnTime = global::UnityEngine.Time.time;
		StartCoroutine("TimeTillJumpscare", timeTillJumpscare);
	}

	public void Fall()
	{
		StopCoroutine("TimeTillJumpscare");
		_animator.speed = 1f;
		_animator.ResetTrigger(ClimbId);
		_animator.ResetTrigger(JumpscareId);
		_animator.SetTrigger(FallId);
		_isHidden = true;
	}

	public void Jumpscare()
	{
		ResetPosition();
		_animator.speed = 1f;
		_animator.ResetTrigger(ClimbId);
		_animator.SetTrigger(JumpscareId);
	}

	public void Hide()
	{
		if (_initialized)
		{
			StopCoroutine("TimeTillJumpscare");
			MoveToEdge();
			_animator.speed = 1f;
			_animator.ResetTrigger(JumpscareId);
			_animator.ResetTrigger(ClimbId);
			_animator.ResetTrigger(FallId);
			_animator.SetTrigger(HideId);
			_isHidden = true;
			base.gameObject.SetActive(value: false);
		}
	}

	public void SetClimbSpeed(float speed)
	{
		_climbSpeed = speed;
	}

	private void MoveToEdge()
	{
		float x = ((orientation == Minireena.Orientation.Left) ? 0f : ((float)_pixelWidth + horizontalOffset * (float)_pixelWidth));
		float y = verticalOffset * (float)_pixelHeight;
		_worldPoint = targetCamera.ScreenToWorldPoint(new global::UnityEngine.Vector3(x, y, distanceOffset));
		base.transform.position = _worldPoint;
	}

	private void ResetPosition()
	{
		base.transform.localPosition = global::UnityEngine.Vector3.zero;
	}

	private global::System.Collections.IEnumerator TimeTillJumpscare(float time)
	{
		yield return new global::UnityEngine.WaitForSeconds(time);
		Jumpscare();
	}

	public bool IsHidden()
	{
		return _isHidden;
	}

	public float TimeSinceSpawn()
	{
		return SpawnTime - global::UnityEngine.Time.time;
	}

	public Minireena()
	{
		timeTillJumpscare = 4f;
		_climbSpeed = 1f;
		_isHidden = true;
	}
}
