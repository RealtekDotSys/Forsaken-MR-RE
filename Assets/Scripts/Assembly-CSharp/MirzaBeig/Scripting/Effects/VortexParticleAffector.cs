namespace MirzaBeig.Scripting.Effects
{
	public class VortexParticleAffector : global::MirzaBeig.Scripting.Effects.ParticleAffector
	{
		private global::UnityEngine.Vector3 axisOfRotation;

		[global::UnityEngine.Header("Affector Controls")]
		public global::UnityEngine.Vector3 axisOfRotationOffset = global::UnityEngine.Vector3.zero;

		protected override void Awake()
		{
			base.Awake();
		}

		protected override void Start()
		{
			base.Start();
		}

		protected override void Update()
		{
			base.Update();
		}

		protected override void LateUpdate()
		{
			base.LateUpdate();
		}

		private void UpdateAxisOfRotation()
		{
			axisOfRotation = global::UnityEngine.Quaternion.Euler(axisOfRotationOffset) * base.transform.up;
		}

		protected override void PerParticleSystemSetup()
		{
			UpdateAxisOfRotation();
		}

		protected override global::UnityEngine.Vector3 GetForce()
		{
			return global::UnityEngine.Vector3.Normalize(global::UnityEngine.Vector3.Cross(axisOfRotation, parameters.scaledDirectionToAffectorCenter));
		}

		protected override void OnDrawGizmosSelected()
		{
			if (base.enabled)
			{
				base.OnDrawGizmosSelected();
				global::UnityEngine.Gizmos.color = global::UnityEngine.Color.red;
				global::UnityEngine.Vector3 vector;
				if (global::UnityEngine.Application.isPlaying && base.enabled)
				{
					UpdateAxisOfRotation();
					vector = axisOfRotation;
				}
				else
				{
					vector = global::UnityEngine.Quaternion.Euler(axisOfRotationOffset) * base.transform.up;
				}
				global::UnityEngine.Gizmos.DrawLine(base.transform.position + offset, base.transform.position + offset + vector * base.scaledRadius);
			}
		}
	}
}
