namespace MirzaBeig.Scripting.Effects
{
	[global::UnityEngine.AddComponentMenu("Effects/Particle Force Fields/Vortex Particle Force Field")]
	public class VortexParticleForceField : global::MirzaBeig.Scripting.Effects.ParticleForceField
	{
		private global::UnityEngine.Vector3 axisOfRotation;

		[global::UnityEngine.Header("ForceField Controls")]
		[global::UnityEngine.Tooltip("Internal offset for the axis of rotation.\n\nUseful if the force field and particle system are on the same game object, and you need a seperate rotation for the system, and the affector, but don't want to make the two different game objects.")]
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
			return global::UnityEngine.Vector3.Normalize(global::UnityEngine.Vector3.Cross(axisOfRotation, parameters.scaledDirectionToForceFieldCenter));
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
				global::UnityEngine.Vector3 vector2 = base.transform.position + center;
				global::UnityEngine.Gizmos.DrawLine(vector2, vector2 + vector * base.scaledRadius);
			}
		}
	}
}
